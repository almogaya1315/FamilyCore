
use [FCore.DB]

go
create procedure stp_AsList
	@first nvarchar(40), @second nvarchar(40) = null,
	@third nvarchar(40) = null, @fourth nvarchar(40) = null
as
select dbo.fn_AsList(@first, @second, @third, @fourth) as 


go
create function fn_AsList(
	@first nvarchar(40), @second nvarchar(40) = null,
	@third nvarchar(40) = null, @fourth nvarchar(40) = null)
returns nvarchar(4000)
with execute as caller
as
begin
return @first + coalesce(' ' + @second, '') 
			  + coalesce(' ' + @third, '') 
			  + coalesce(' ' + @fourth, '') 
end