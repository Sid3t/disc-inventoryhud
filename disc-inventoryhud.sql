create table esx_essentialmode.disc_inventory
(
	id int auto_increment
		primary key,
	owner varchar(50) not null,
	type varchar(10) null,
	slot int not null,
	data longtext not null
);

create index disc_inventory_owner_type_index
	on esx_essentialmode.disc_inventory (owner, type);

create table esx_essentialmode.disc_inventory_itemdata
(
	id bigint unsigned auto_increment,
	item varchar(50) not null,
	description text not null,
	label varchar(50) not null,
	meta tinyint(1) default 0 not null,
	itemurl text not null,
	close tinyint(1) default 0 not null,
	constraint disc_inventory_itemdata_item_uindex
		unique (item),
	constraint id
		unique (id)
);

