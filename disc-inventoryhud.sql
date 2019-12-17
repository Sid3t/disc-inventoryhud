create table if not exists esx_essentialmode.disc_inventory
(
	id int auto_increment
		primary key,
	owner text not null,
	type text null,
	slot int not null,
	data longtext not null
);

