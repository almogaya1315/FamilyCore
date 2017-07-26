
use [FCore.DB]

go
create procedure stp_AddVideo
	@libId int,
	@desc nvarchar(50),
	@path nvarchar(100)

as 
	insert into dbf.Videos values(@libId, @desc, @desc)

	 