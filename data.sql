INSERT INTO Customer (FirstName, LastName) VALUES('Jameka', 'Echols');
INSERT INTO Customer (FirstName, LastName) VALUES('Ali', 'Abdulle');
INSERT INTO Customer (FirstName, LastName) VALUES('Brian', 'Jobe');
INSERT INTO Customer (FirstName, LastName) VALUES('Billy', 'Mathison');

INSERT INTO ProductType (Name) VALUES('Appliances');
INSERT INTO ProductType (Name) VALUES('Electronics');
INSERT INTO ProductType (Name) VALUES('Books');

INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, [Description], Quantity) VALUES (1, 2, 550.00, 'Oven', 'General Electronics create an all around oven for the best bakers around.', 2);
INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, [Description], Quantity) VALUES (2, 1, 3600.99, '2019 MacBook Pro', 'Apple presents this new MacBook pro with new a new i8 processor', 5);
INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, [Description], Quantity) VALUES (3, 4, 14.00, 'Lovecraft Country', 'In the Jim Crow era, a group of individuals live through the terrors of America.', 19);
INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, [Description], Quantity) VALUES (3, 3, 11.50, 'Harry Potter and the Half-blood Prince', 'Another installation of the Wizarding World from author, J.K. Rowling, deliverying yet another magical piece of literature.', 21);

INSERT INTO PaymentType (AcctNumber, Name, CustomerId) VALUES (22222, 'Test', 1) 
INSERT INTO PaymentType (AcctNumber, Name, CustomerId) VALUES (12345, 'Visa', 1) 
INSERT INTO PaymentType (AcctNumber, Name, CustomerId) VALUES (33333, 'Test', 1) 
INSERT INTO PaymentType (AcctNumber, Name, CustomerId) VALUES (55555, 'PayPal', 1)

INSERT INTO [Order] (CustomerId, PaymentTypeId) VALUES (1,2);
INSERT INTO [Order] (CustomerId, PaymentTypeId) VALUES (2,2);
INSERT INTO [Order] (CustomerId, PaymentTypeId) VALUES (3,3);
INSERT INTO [Order] (CustomerId, PaymentTypeId) VALUES (4,4);
INSERT INTO [Order] (CustomerId, PaymentTypeId) VALUES (2, null);

INSERT INTO OrderProduct (OrderId, ProductId) VALUES (1,4);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (2,4);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (3,3);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (4,2);

INSERT INTO Department ([Name], Budget) VALUES ('Customer Experience', 450000);
INSERT INTO Department ([Name], Budget) VALUES ('Engineering', 890000);
INSERT INTO Department ([Name], Budget) VALUES ('Human Resources', 500000);

INSERT INTO Computer (PurchaseDate, DecomissionDate, Make, Manufacturer) VALUES (1/23/2015, 9/23/2018, 'Inspiron 1500', 'Dell');
INSERT INTO Computer (PurchaseDate, DecomissionDate, Make, Manufacturer) VALUES (2/15/2017, null,'Aspire E 15', 'Acer');
INSERT INTO Computer (PurchaseDate, DecomissionDate, Make, Manufacturer) VALUES (10/5/2016, null, 'Chromebook C434', 'Asus');
INSERT INTO Computer (PurchaseDate, DecomissionDate, Make, Manufacturer) VALUES (12/24/2013, 4/21/2019, 'Yogo C930', 'Lenovo');
INSERT INTO Computer (PurchaseDate, DecomissionDate, Make, Manufacturer) VALUES (7/2/2012, 5/08/2019, 'Area-51m', 'Alienware');
INSERT INTO Computer (PurchaseDate, DecomissionDate, Make, Manufacturer) VALUES (6/12/2016, null, 'XPS 30', 'Dell');
INSERT INTO Computer (PurchaseDate, DecomissionDate, Make, Manufacturer) VALUES (9/1/2015, null, 'ThinkPad X1', 'Lenovo');
INSERT INTO Computer (PurchaseDate, DecomissionDate, Make, Manufacturer) VALUES (11/04/2018, null, 'MacBook Pro 13in', 'Apple');
INSERT INTO Computer (PurchaseDate, DecomissionDate, Make, Manufacturer) VALUES (3/11/2017, null, 'MateBook X', 'Huawei');

INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Andy', 'Collins', 1, 1);
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Jisie', 'Davis', 2, 1);
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Steve', 'Brownlee', 3, 1);
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Leah', 'Hoefling', 2, 0);
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Kristen', 'Norris', 1, 0);
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) VALUES ('Madi', 'Peper', 3, 0);

INSERT INTO TrainingProgram ([Name], StartDate, EndDate, MaxAttendees) VALUES('Diversity & Inclusion','2019-04-25T14:00:00', '2019-04-25T17:00:00', 150);
INSERT INTO TrainingProgram ([Name], StartDate, EndDate, MaxAttendees) VALUES('Customer Complaint', '2019-12-02T08:30:00', '2019-12-03T17:00:00', 200);
INSERT INTO TrainingProgram ([Name], StartDate, EndDate, MaxAttendees) VALUES('Safety in the Workplace', '2019-08-02T08:30:00', '2019-08-05T17:00:00', 100);


INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (1,2, 12/24/2018, null);
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (2,3, 9/01/2017, null);
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (3,6, 8/12/2017, null);
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (4,7, 1/20/2017, 10/02/2019);
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (5,8, 12/10/2018, null);
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate, UnassignDate) VALUES (6,9, 3/15/2018, null);

INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (2,2);
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (1,3);
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (6,1);