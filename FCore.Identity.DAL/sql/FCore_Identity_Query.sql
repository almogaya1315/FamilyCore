
use [FCore.DB.UserIdentity]

select *
from dbo.AspNetUsers

update dbo.AspNetUsers
set Password = 'lkga1315'
where UserName = 'Lior'

update dbo.AspNetUsers
set MemberId = 1
where UserName = 'Lior'

delete 
from dbo.AspNetUsers
where MemberId = 25 