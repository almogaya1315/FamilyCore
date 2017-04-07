
use [FCore.DB]

select *
from dbf.Families

select *
from dbf.ContactBooks

select *
from dbf.ContactInfoes

select *
from dbf.FamilyMembers

select *
from dbf.Permissions

select *
from dbf.Albums

select *
from dbf.images

select *
from dbf.Relatives

select m.FirstName, m.LastName, r.Relationship, mr.FirstName, mr.LastName 
from dbf.FamilyMembers m join dbf.Relatives r on m.Id = r.MemberId
						 join dbf.FamilyMembers mr on mr.Id = r.RelativeId 

select *
from dbf.ChatGroups

select *
from dbf.Messages

select *
from dbf.VideoLibraries

select *
from dbf.Videos

--delete 
--from dbf.Relatives
--where Id > 0

--delete 
--from dbf.ContactInfoes
--where Id = 2

--delete
--from dbf.FamilyMembers
--where Id = 3