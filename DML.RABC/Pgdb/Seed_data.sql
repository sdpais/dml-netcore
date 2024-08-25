
TRUNCATE TABLE Clients RESTART IDENTITY CASCADE;

INSERT INTO Clients (ClientName) VALUES ('Test Client');
INSERT INTO Clients (ClientName, IsActive) VALUES ('InActive Client', false);
INSERT INTO Clients (ClientName) VALUES ('Another Client');

INSERT INTO Modules (ModuleName, ClientId) VALUES ('Test Module', (Select Id From Clients Where ClientName = 'Test Client'));
INSERT INTO Modules (ModuleName, ClientId, IsActive) VALUES ('Inactive Module', (Select Id From Clients Where ClientName = 'InActive Client'), false);
INSERT INTO Modules (ModuleName, ClientId) VALUES ('Another Module', (Select Id From Clients Where ClientName = 'Another Client'));

INSERT INTO Entities (EntityName, ClientId, ModuleId) VALUES ('Test Entity', (Select Id From Clients Where ClientName = 'Test Client'), 
	(Select Id From Modules Where ModuleName = 'Test Module'));
INSERT INTO Entities (EntityName, ClientId, ModuleId, IsActive) VALUES ('Inactive Entity', (Select Id From Clients Where ClientName = 'InActive Client'), 
	(Select Id From Modules Where ModuleName = 'Inactive Module'), false);
INSERT INTO Entities (EntityName, ClientId, ModuleId) VALUES ('Another Module', (Select Id From Clients Where ClientName = 'Another Client'), 
	(Select Id From Modules Where ModuleName = 'Another Module'));
