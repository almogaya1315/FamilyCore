
use [FCore.DB.UserIdentity]

select *
from dbo.AspNetUsers

delete 
from dbo.AspNetUsers
where UserName = 'Lior' 