create table TEAMS
(
teamid int not null identity(1,1) primary key,
Logo nvarchar(max),
[Team Name] nvarchar(50)
)
drop table TEAMS
create table USERS(
id uniqueidentifier not null primary key,
NAME nvarchar(50),
SURNAME nvarchar(50),
PASSWORD nvarchar(50),
ROLE int,
TELEPHONE nvarchar(13),
[Favourite team] int constraint FAV_TEAM_FK foreign key references TEAMS(teamid)
)
--create table TOURNAMENT(
--teamID int not null constraint TEAMTOUR_PK primary key references TEAMS(teamid),
--Logo nvarchar(max),
--Team nvarchar(50),
--Games int,
--W int,
--L int,
--D int,
--P int
--)
--drop table TOURNAMENT
select * into TOURNAMENT from TEAMS
alter table TOURNAMENT add W int, T int, L int, P int
create table PLAYERS(
teamID int not null constraint TEAMTOUR_FK foreign key references TEAMS(teamid),
[Number] int not null,
Photo nvarchar(max),
[Name] nvarchar(max),
PositionID nvarchar(50),
Country nvarchar(50)
)
create table Articles(
id uniqueidentifier not null primary key,
Header nvarchar(50),
Image nvarchar(50),
[Creation Date] date,
)
create table Bonus(
id uniqueidentifier not null primary key,
id_user uniqueidentifier constraint ORDERER_FK foreign key references USERS(id),
status varchar(30)
)
create table MATCHES
(Team1 int constraint TEAM1_FK foreign key references TEAMS(teamid),
Logo1 nvarchar(max),
Goals1 int default 0,
Goals2 int default 0,
Logo2 nvarchar(max),
Team2 int constraint TEAM2_FK foreign key references TEAMS(teamid)
)
alter table MATCHES add [GAME_DATE] datetime
drop table matches
insert into MATCHES select ht.[teamid] as hometeam, ht.[Logo], 0 as homegoals,0 as guestgoals, gt.[Logo], gt.[teamid] as gt from TEAMS ht join TEAMS gt on 1=1 where ht.teamid!=gt.teamid
select * from matches order by [GAME_DATE]
alter table matches add matchid int not null identity(1,1) primary key
create table Bonus(
id uniqueidentifier not null primary key,
userId uniqueidentifier not null constraint USER_FK foreign key references USERS(id),
name nvarchar(max)
)