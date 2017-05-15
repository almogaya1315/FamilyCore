
use [FCore.DB]

select * --Model
from dbo.__MigrationHistory

delete 
from dbo.__MigrationHistory
where MigrationId = '201704302004215_perm_admin'

select *
from dbf.Families

select *
from dbf.ContactBooks

select *
from dbf.ContactInfoes

delete 
from dbf.ContactInfoes
where Id = 7

select *
from dbf.FamilyMembers

delete 
from dbf.FamilyMembers
where Id = 13

alter table dbf.FamilyMembers
add Gender varchar not null default('')

alter table dbf.FamilyMembers
drop column Gender

update dbf.FamilyMembers
set Gender = 'Male'
where Id = 1

update dbf.FamilyMembers
set Gender = 'Female'
where Id = 2

alter table dbf.FamilyMembers
drop column Gender

update dbf.FamilyMembers
set ProfileImagePath = ''
where Id = 1

select *
from dbf.Permissions

delete 
from dbf.Permissions
where Id != 1041 and Id != 1042

alter table dbf.permissions
drop column Admin 

alter table dbf.permissions
add Admin bit not null default(0)

alter table dbf.Permissions
drop column Admin 

delete 
from dbf.Permissions
where id != 1019 and id != 1020

select *
from dbf.Albums

select *
from dbf.images

select *
from dbf.Relatives

delete
from dbf.Relatives
where Id = 3 or Id = 4

update dbf.Relatives
set Relationship = 'Wife'
where Id = 1

update dbf.Relatives
set Relationship = 'Husband'
where Id = 2

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