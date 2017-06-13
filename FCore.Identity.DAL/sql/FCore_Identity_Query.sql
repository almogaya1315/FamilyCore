
use [FCore.DB.UserIdentity]

select *
from dbo.AspNetUsers

update dbo.AspNetUsers
set Password = 'lkga1315'
where UserName = 'Lior'

update dbo.AspNetUsers
set Password = 'Lkga1315!'
where MemberId = 1

delete 
from dbo.AspNetUsers
where MemberId = 0