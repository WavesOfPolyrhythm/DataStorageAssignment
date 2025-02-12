--SELECT
--SELECT * FROM Employees;
--SELECT * FROM Customers;
--SELECT * FROM Unit;
--SELECT * FROM Status
--SELECT * FROM Roles
--SELECT * FROM CustomerContact
--SELECT * FROM Services
--SELECT * FROM Projects


--INSERT
--INSERT INTO Customers VALUES ('ABC Data')
--INSERT INTO Roles (RoleName) VALUES ('Project Manager');
--INSERT INTO Employees (Name, Email, RoleId) VALUES ('John Doe', 'john.doe@gmail.com', 1)
--INSERT INTO Unit VALUES ('Fixed', 'Fixed agreement for service')
--INSERT INTO Services (Name, Price, UnitId) VALUES ('Web Development', 15000, 2);
--INSERT INTO Status VALUES ('Not Started')
--INSERT INTO Projects VALUES ('Website Project', 'Building a website for ABC Data', '2024-03-01', '2024-06-01', 15000, 5, 1, 2, 1);
--INSERT INTO CustomerContacts VALUES ('Martin Bergsten', '070000000', 'martin@abcdata.com', 1)

/*
SELECT 
    CONCAT('P-', p.Id) AS 'ProjectId',
    p.Title,
    p.Description,
    p.StartDate,
    p.EndDate,
    s.StatusName AS 'CurrentStatus',
    c.CustomerName,
    CONCAT(e.Name, ' (', e.Email, ')') AS 'ProjectManager',
    CONCAT(cc.Name, ' (', cc.Email, ')') AS 'CustomerContact'
    
FROM Projects p
JOIN Status s ON p.StatusId = s.Id
JOIN Customers c ON p.CustomerId = c.Id
JOIN Employees e ON p.EmployeeId = e.Id
LEFT JOIN CustomerContacts cc ON c.Id = cc.CustomerId;
*/
--DELETE FROM Customers WHERE Id = 12;

/*
SELECT 
    CONCAT('P-', p.Id) AS 'ProjectId',
    p.Title,
    p.Description,
    p.StartDate,
    p.EndDate,
    s.StatusName AS 'CurrentStatus',
    c.CustomerName,
    CONCAT(e.Name, ' (', e.Email, ')') AS 'ProjectManager',
    CONCAT(cc.Name, ' (', cc.Email, ')') AS 'CustomerContact'
FROM Projects p
JOIN Status s ON p.StatusId = s.Id
JOIN Customers c ON p.CustomerId = c.Id
JOIN Employees e ON p.EmployeeId = e.Id
LEFT JOIN CustomerContacts cc ON c.Id = cc.CustomerId
WHERE p.Id = 19; */

