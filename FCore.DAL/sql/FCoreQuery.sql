
use [FCore.DB]

select * --Model
from dbo.__MigrationHistory

delete 
from dbo.__MigrationHistory
where MigrationId = '201704302004215_perm_admin'

select *
from dbf.Families

delete 
from dbf.Families
where id > 0

insert into dbf.Families 
values('Matsliah')

select *
from dbf.ContactBooks

insert into dbf.ContactBooks
values(12, 'Matsliah')

delete
from dbf.ContactBooks
where Id > 0

select *
from dbf.ContactInfoes

insert into dbf.ContactInfoes
values(8, 'Lior Matsliah', 'Israel', 'Petah-Tikva', 'Haportsim', 16, '052-3751421', 'liormatsliah1985@gmail.com', 1018)

delete 
from dbf.ContactInfoes
where Id > 0

select *
from dbf.FamilyMembers

insert into dbf.FamilyMembers
values(12, 1055, null, 'Lior', 'Matsliah', 23/5/1985, 'Beilinson hospital, Petah-Tikva', '~\Images\Profiles\Photo0306.jpg', 'The founder of FAMILY-CORE', 'Male')

update dbf.FamilyMembers
set ContactInfoId = 1014
where id = 1018

delete 
from dbf.FamilyMembers
where Id > 0

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

insert into dbf.Permissions
values (0, 0, 0, 0)

delete 
from dbf.Permissions
where Id = 1066 or Id = 1067 or Id = 1068 or Id = 1069 or Id = 1071 or Id = 1072

delete 
from dbf.Permissions
where Id > 1051 

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

insert into dbf.Albums
values('First album', 1, 'Matsliah')

delete
from dbf.Albums
where id > 0

select *
from dbf.images

insert into dbf.images
values(2, 'First image', '~/Images/first.jpg')

delete
from dbf.images
where id > 0

select *
from dbf.Relatives

delete
from dbf.Relatives
where Id > 0

insert into dbf.Relatives
values (1, 2, 'Wife')

insert into dbf.Relatives
values (2, 1, 'Husband')


update dbf.Relatives
set Relationship = 'Wife'
where Id = 1021

update dbf.Relatives
set Relationship = 'Husband'
where Id = 2

select m.FirstName, m.LastName, r.Relationship, mr.FirstName, mr.LastName 
from dbf.FamilyMembers m join dbf.Relatives r on m.Id = r.MemberId
						 join dbf.FamilyMembers mr on mr.Id = r.RelativeId 

select *
from dbf.ChatGroups

insert into dbf.ChatGroups
values('First chat', 1, 1)

delete
from dbf.ChatGroups
where Id > 0

select *
from dbf.Messages

insert into dbf.Messages
values(2, 1, 26, 'First Message') 

delete
from dbf.Messages
where Id > 0

select *
from dbf.VideoLibraries

insert into dbf.VideoLibraries
values('First library', 1, 'Matsliah')

delete
from dbf.VideoLibraries
where Id > 0

select *
from dbf.Videos

insert into dbf.Videos
values(2, 'Video3', '~/Videos/libId2/MOV00290.MP4')

delete
from dbf.Videos
where Id > 0

delete 
from dbf.Relatives
where Id > 0

delete 
from dbf.ContactInfoes
where Id > 0

delete
from dbf.FamilyMembers
where Id > 1