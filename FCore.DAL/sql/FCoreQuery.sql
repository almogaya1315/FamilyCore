
use [FCore.DB]

select * --Model
from dbo.__MigrationHistory

delete 
from dbo.__MigrationHistory
where MigrationId = '201704302004215_perm_admin'

select *
from dbf.Families

insert into dbf.Families 
values('Perry')

select *
from dbf.ContactBooks

select *
from dbf.ContactInfoes

delete 
from dbf.ContactInfoes
where Id = 8

select *
from dbf.FamilyMembers

delete 
from dbf.FamilyMembers
where Id = 8

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
where Id = 1066 or Id = 1067 or Id = 1068 or Id = 1069 or Id = 1071 or Id = 1072

delete 
from dbf.Permissions
where Id != 1073 and Id != 1074 and Id = 1070 and Id = 1075

alter table dbf.permissions
drop column Admin 

alter table dbf.permissions
add Admin bit not null default(0)

alter table dbf.Permissions
drop column Admin 

delete 
from dbf.Permissions
where id != 38 and id != 39

select *
from dbf.Albums

select *
from dbf.images

select *
from dbf.Relatives

delete
from dbf.Relatives
where Id != 1 and Id != 2

insert into dbf.Relatives
values (1, 2, 'Wife')

insert into dbf.Relatives
values (2, 1, 'Husband')


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