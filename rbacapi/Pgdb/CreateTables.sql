CREATE TABLE 
	IF NOT EXISTS Clients (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientName varchar(255) NOT NULL UNIQUE,
	IsActive boolean NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
    CONSTRAINT Pk_Client_id PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS Users (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientId bigint NOT NULL,
	UserName varchar(255) NOT NULL UNIQUE,
	Password varchar(255) NOT NULL,
	IsActive boolean NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	CONSTRAINT Pk_User_id PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS Roles (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientId bigint NOT NULL,
	IsActive boolean NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	RoleName varchar(255) NOT NULL UNIQUE,
	CONSTRAINT Pk_Role_id PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS Groups (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientId bigint NOT NULL,
	IsActive boolean NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	GroupName varchar(255) NOT NULL UNIQUE,
	CONSTRAINT Pk_Group_id PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS UserGroups (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientId bigint NOT NULL,
	UserId bigint NOT NULL,
	GroupId bigint NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	CONSTRAINT Pk_UserGroup_id PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS UserRoles (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientId bigint NOT NULL,
	UserId bigint NOT NULL,
	RoleId bigint NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	CONSTRAINT Pk_UserRole_id PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS GroupRoles (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientId bigint NOT NULL,
	GroupId bigint NOT NULL,
	RoleId bigint NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	CONSTRAINT Pk_GroupRole_id PRIMARY KEY(Id)
);


CREATE TABLE IF NOT EXISTS Entities (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientId bigint NOT NULL,
	EntityName varchar(255) NOT NULL UNIQUE,
	ModuleId bigint NOT NULL,
	IsActive boolean NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	CONSTRAINT Pk_Entitie_id PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS EntityRoles (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientId bigint NOT NULL,
	EntityId bigint NOT NULL,
	RoleId bigint NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	CONSTRAINT Pk_EntityRole_id PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS Modules (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientId bigint NOT NULL,
	ModuleName varchar(255) NOT NULL UNIQUE,
	IsActive boolean NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	CONSTRAINT Pk_Module_id PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS EntityActions (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientId bigint NOT NULL,
	EntityId bigint NOT NULL,
	Edit boolean NOT NULL,
	View boolean NOT NULL,
	Delete boolean NOT NULL,
	Search boolean NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	CONSTRAINT Pk_EntityAction_id PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS EntityRoleActions (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientId bigint NOT NULL,
	EntityActionId bigint NOT NULL,
	RoleId bigint NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	CONSTRAINT Pk_EntityRoleAction_id PRIMARY KEY(Id)
);
CREATE TABLE IF NOT EXISTS EntityAttributes (
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	ClientId bigint NOT NULL,
	AttributeName varchar(255) NOT NULL,
	EntityId bigint NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	ControlName varchar(255) NOT NULL,
	CONSTRAINT Pk_EntityAttribute_id PRIMARY KEY(Id)
);

CREATE TABLE IF NOT EXISTS EntityRoleAttributes (
	RoleId bigint NOT NULL,
	ClientId bigint NOT NULL,
	Id bigint GENERATED ALWAYS AS IDENTITY NOT NULL UNIQUE,
	EntityId bigint NOT NULL,
	EntityAttributeId bigint NOT NULL,
	Edit boolean NOT NULL,
	View boolean NOT NULL,
	Delete boolean NOT NULL,
	CreatedOn Date DEFAULT CURRENT_DATE,
	UpdatedOn Date DEFAULT CURRENT_DATE,
	CONSTRAINT Pk_EntityRoleAttribute_id PRIMARY KEY(Id)
);



ALTER TABLE UserGroups ADD CONSTRAINT UserGroups_fk1 FOREIGN KEY (UserId) REFERENCES Users(Id);

ALTER TABLE UserGroups ADD CONSTRAINT UserGroups_fk2 FOREIGN KEY (GroupId) REFERENCES Groups(Id);
ALTER TABLE UserRoles ADD CONSTRAINT UserRoles_fk1 FOREIGN KEY (UserId) REFERENCES Users(Id);

ALTER TABLE UserRoles ADD CONSTRAINT UserRoles_fk2 FOREIGN KEY (RoleId) REFERENCES Roles(Id);
ALTER TABLE GroupRoles ADD CONSTRAINT GroupRoles_fk1 FOREIGN KEY (GroupId) REFERENCES Groups(Id);

ALTER TABLE GroupRoles ADD CONSTRAINT GroupRoles_fk2 FOREIGN KEY (RoleId) REFERENCES Roles(Id);

ALTER TABLE Entities ADD CONSTRAINT Entities_fk2 FOREIGN KEY (ModuleId) REFERENCES Modules(Id);

ALTER TABLE EntityRoles ADD CONSTRAINT EntityRoles_fk1 FOREIGN KEY (EntityId) REFERENCES Entities(Id);
ALTER TABLE EntityRoles ADD CONSTRAINT EntityRoles_fk2 FOREIGN KEY (RoleId) REFERENCES Roles(Id);

ALTER TABLE EntityActions ADD CONSTRAINT EntityAction_fk1 FOREIGN KEY (EntityId) REFERENCES Entities(Id);

ALTER TABLE EntityRoleActions ADD CONSTRAINT EntityRoleAction_fk1 FOREIGN KEY (EntityActionId) REFERENCES EntityActions(Id);
ALTER TABLE EntityRoleAttributes ADD CONSTRAINT EntityRoleAttribute_fk1 FOREIGN KEY (EntityAttributeId) REFERENCES EntityAttributes(Id);
ALTER TABLE EntityRoleActions ADD CONSTRAINT EntityRoleAction_fk2 FOREIGN KEY (RoleId) REFERENCES Roles(Id);

ALTER TABLE Users ADD CONSTRAINT UsersClients_fk FOREIGN KEY (ClientId) REFERENCES Clients(Id);
ALTER TABLE Groups ADD CONSTRAINT GroupsClients_fk FOREIGN KEY (ClientId) REFERENCES Clients(Id);
ALTER TABLE Roles ADD CONSTRAINT RolesClients_fk FOREIGN KEY (ClientId) REFERENCES Clients(Id);
ALTER TABLE Modules ADD CONSTRAINT ModulesClients_fk FOREIGN KEY (ClientId) REFERENCES Clients(Id);
ALTER TABLE UserGroups ADD CONSTRAINT UserGroupsClients_fk FOREIGN KEY (ClientId) REFERENCES Clients(Id);
ALTER TABLE UserRoles ADD CONSTRAINT UserRolesClients_fk1 FOREIGN KEY (ClientId) REFERENCES Clients(Id);
ALTER TABLE GroupRoles ADD CONSTRAINT GroupRolesClients_fk1 FOREIGN KEY (ClientId) REFERENCES Clients(Id);
ALTER TABLE Entities ADD CONSTRAINT EntitiesClients_fk2 FOREIGN KEY (ClientId) REFERENCES Clients(Id);
ALTER TABLE EntityAttributes ADD CONSTRAINT EntityAttributeClients_fk1 FOREIGN KEY (ClientId) REFERENCES Clients(Id);
ALTER TABLE EntityRoles ADD CONSTRAINT EntityRolesClients_fk1 FOREIGN KEY (ClientId) REFERENCES Clients(Id);
ALTER TABLE EntityActions ADD CONSTRAINT EntityActionClients_fk1 FOREIGN KEY (ClientId) REFERENCES Clients(Id);
ALTER TABLE EntityRoleActions ADD CONSTRAINT EntityRoleActionClients_fk1 FOREIGN KEY (ClientId) REFERENCES Clients(Id);
ALTER TABLE EntityRoleAttributes ADD CONSTRAINT EntityRoleAttributeClients_fk1 FOREIGN KEY (ClientId) REFERENCES Clients(Id);



