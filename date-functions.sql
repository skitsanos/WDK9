create function YEARS_OLD (@birthDay datetime, @date datetime)
returns smallint
as
begin
declare @yearDiff smallint
select @yearDiff=DATEDIFF (yyyy, @birthDay, @date)
if @date <   cast(
  cast(year(@date) as varchar(4))+
  '-'+
  cast(month(@birthDay) as varchar(2))+
  '-'+
  cast(day(@birthDay) as varchar(2))+' 00:00:00.0' as datetime)
   set @yearDiff=@yearDiff-1
 return (@yearDiff)
end

GO

create function UNIX_TIMESTAMP(@date datetime)
returns int
as
begin
declare @deltaGMT smallint, @unixtime int

exec master.dbo.xp_regread 'HKEY_LOCAL_MACHINE',
'SYSTEM\CurrentControlSet\Control\TimeZoneInformation',
'ActiveTimeBias', @DeltaGMT OUT
set @unixtime=(select datediff(ss, dateadd(minute, -@deltaGMT, '1970-01-01 00:00:00.0'), @date))
return @unixtime
end

GO

create function FROM_UNIXTIME(@unixtime int)
returns datetime
as
begin
  declare @deltaGMT int, @date datetime
  exec master.dbo.xp_regread 'HKEY_LOCAL_MACHINE', 
'SYSTEM\CurrentControlSet\Control\TimeZoneInformation',
'ActiveTimeBias', @deltaGMT OUT
  set @date=(select dateadd(second, @unixtime, dateadd(minute, -@deltaGMT, '1970-01-01 00:00:00.0')))
return @date
end

GO

create function GMT()
returns tinyint
as
begin
declare @deltaGMT smallint, @GMT tinyint

exec master.dbo.xp_regread 'HKEY_LOCAL_MACHINE',
'SYSTEM\CurrentControlSet\Control\TimeZoneInformation',
'ActiveTimeBias', @deltaGMT OUT
set @GMT=(select -@deltaGMT/60)
return @GMT
end

GO

create function SEC_TO_TIME(@sec int)
returns varchar(11)
as
begin
  declare @hour smallint, @minute tinyint, @second tinyint
  set @hour=(select floor(@sec/3600))
  set @minute=(select floor((@sec-@hour*3600)/60))
  set @second=(select (@sec-@hour*3600-@minute*60))
return cast(@hour as varchar(5))+':'+cast(@minute as varchar(2))+':'+cast(@second as varchar(2))
end

GO

create function TIME_TO_SEC(@time varchar(11))
returns int
as
begin
  declare @sec int, @hour smallint, @minute tinyint, @second tinyint, @index1 tinyint, @index2 tinyint
  set @index1=PATINDEX('%:%', @time)
  set @hour=cast((select LEFT(@time, @index1-1)) as smallint)
  set @index2=PATINDEX('%:%', RIGHT(@time, LEN(@time)-@index1))
  set @index2=LEN(@time)-@index2
  set @minute=cast((select SUBSTRING(@time, @index1+1, (@index2-@index1))) as tinyint)
  set @second=cast((select RIGHT(@time, LEN(@time)-@index2-1)) as tinyint)

return @hour*3600+@minute*60+@second

end

GO

create function SEASON(@date datetime)
returns tinyint
as
begin
 declare @month tinyint, @season tinyint
 set @month=(select MONTH(@date))
 if @month>11
   set @month=0
 set @season=(FLOOR(@month/3)+1)
return @season
end

--- part 2, added April 22, 2009

create  function DateOnly(@DateTime DateTime)
-- Returns @DateTime at midnight; i.e., it removes the time portion of a DateTime value.
returns datetime
as
    begin
    return dateadd(dd,0, datediff(dd,0,@DateTime))
    end
go

create function Date(@Year int, @Month int, @Day int)
-- returns a datetime value for the specified year, month and day
returns datetime
as
    begin
    return dateadd(month,((@Year-1900)*12)+@Month-1,@Day-1)
    end
go

create function Time(@Hour int, @Minute int, @Second int)
-- Returns a datetime value for the specified time at the "base" date (1/1/1900)
returns datetime
as
    begin
    return dateadd(ss,(@Hour*3600)+(@Minute*60)+@Second,0)
    end
go

create function TimeOnly(@DateTime DateTime)
-- returns only the time portion of a DateTime, at the "base" date (1/1/1900)
returns datetime
as
    begin
    return dateadd(day, -datediff(day, 0, @datetime), @datetime)
    end
go

create function DateTime(@Year int, @Month int, @Day int, @Hour int, @Minute int, @Second int)
-- returns a dateTime value for the date and time specified.
returns datetime
as
    begin
    return dbo.Date(@Year,@Month,@Day) + dbo.Time(@Hour, @Minute,@Second)
    end
go

create function TimeSpan(@Days int, @Hours int, @Minutes int, @Seconds int)
-- returns a datetime the specified # of days/hours/minutes/seconds from the "base" date of 1/1/1900 (a "TimeSpan")
returns datetime
as
    begin
    return dbo.Time(@Hours,@Minutes,@Seconds) + @Days
    end
go

create function TimeSpanUnits(@Unit char(1), @TimeSpan datetime)
-- returns the # of units specified in the TimeSpan.
-- The Unit parameter can be: "d" = days, "h" = hours, "m" = minutes, "s" = seconds
returns int
as
    begin
    return case @Unit
        when 'd' then datediff(day, 0, @TimeSpan)
        when 'h' then datediff(hour, 0, @TimeSpan)
        when 'm' then datediff(minute, 0, @TimeSpan)
        when 's' then datediff(second, 0, @TimeSpan)
        else Null end
    end
go
/*
Here's a TimeSpan usage example:

declare @Deadline datetime -- remember, we still use standard datetimes for everything, include TimeSpans
set @Deadline = dbo.TimeSpan(2,0,0,0)   -- the deadline is two days

declare @CreateDate datetime
declare @ResponseDate datetime

set @CreateDate = dbo.DateTime(2006,1,3,8,30,0)  -- Jan 3, 2006, 8:30 AM
set @ResponseDate = getdate() -- today

-- See if the response date is past the deadline:
select case when @ResponseDate > @CreateDate + @Deadline then 'overdue.' else 'on time.' end as Result

-- Find out how many total hours it took to respond:  
declare @TimeToRepond datetime
set @TimeToRespond = @ResponseDate - @CreateDate

select dbo.TimeSpanUnits('h', @TimeToRespond) as ResponseTotalHours

-- Return the response time as # of days, # of hours, # of minutes:
select dbo.TimeSpanUnits('d',@TimeToRespond) as Days, DatePart(hour, @TimeToRespond) as Hours, DatePart(minute, @TimeToRespond) as Minutes

-- Return two days and two hours from now:
select getdate() + dbo.TimeSpan(2,2,0,0)

*/