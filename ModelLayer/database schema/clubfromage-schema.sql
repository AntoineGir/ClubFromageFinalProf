drop database if exists clubfromage;
create database clubfromage;

use clubfromage;

create table Pays(
	id int primary key auto_increment,
    name varchar(100) not null
) engine InnoDB;

create table Fromage(
	id int primary key auto_increment,
    name varchar(100) not null,
    pays_origine_id int,
    durAffinage int,
    histoire longtext,
    recette longtext,
    creation date,
    image varchar(255),
    constraint foreign key (pays_origine_id) references Pays(id) on delete set null
) engine InnoDB;

create table Membre(
	id int primary key auto_increment,
    username varchar(180),
    email varchar(320),
    enabled boolean,
    password varchar(255),
    lastLogin datetime,
    pseudo varchar(30),
    entryDate date
) engine InnoDB;

create table Avis(
	fromageId int,
    membreId int,
    avis varchar(2000),
    stars tinyint,
    primary key (membreId, fromageId),
    constraint foreign key (fromageId) references Fromage(id),
    constraint foreign key (membreId) references Membre(id)
) engine InnoDB;
