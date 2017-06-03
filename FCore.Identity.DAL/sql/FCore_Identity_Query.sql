
use [FCore.DB.UserIdentity]

select *
from dbo.AspNetUsers

update dbo.AspNetUsers
set MemberId = 1
where UserName = 'Lior'

delete 
from dbo.AspNetUsers
where UserName = 'Lior' 