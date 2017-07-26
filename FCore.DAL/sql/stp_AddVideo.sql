
use [FCore.DB]

go
alter procedure stp_AddVideo
	@libId int,
	@desc nvarchar(50),
	@path nvarchar(100)

as 
	insert into dbf.Videos values(@libId, @desc, @path)

select * 
from dbf.Videos

delete 
from dbf.Videos
where Id = 2033