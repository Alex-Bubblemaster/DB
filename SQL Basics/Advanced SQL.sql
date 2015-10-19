## 05. Advanced SQL
### _Homework_

1.	Write a SQL query to find the names and salaries of the employees that take the minimal salary in the company.
	*	Use a nested `SELECT` statement.
	
SELECT *
FROM Employees 
WHERE Salary = (SELECT MIN (Salary) FROM Employees)

2.	Write a SQL query to find the names and salaries of the employees that have a salary that is up to 10% higher than the minimal salary for the company.

DECLARE @MinSalary int = (SELECT MIN(Salary) FROM Employees)
SELECT *
FROM Employees 
WHERE Salary BETWEEN @MinSalary AND @MinSalary * 1.1

3.	Write a SQL query to find the full name, salary and department of the employees that take the minimal salary in their department.
	*	Use a nested `SELECT` statement.
	
SELECT e.FirstName + ' ' + e.LastName AS Fullname,
	   e.Salary,
	   d.Name
FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID
WHERE Salary = (SELECT MIN(Salary) FROM Employees e
				WHERE e.DepartmentID = d.DepartmentID)
ORDER BY Salary

4.	Write a SQL query to find the average salary in the department #1.

SELECT ROUND(AVG(Salary),2) AS AverageSalary
FROM Employees
WHERE DepartmentID = 1;

5.	Write a SQL query to find the average salary  in the "Sales" department.

SELECT ROUND(AVG(Salary),2) AS AverageSalary
FROM Employees e 
JOIN Departments d ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'

6.	Write a SQL query to find the number of employees in the "Sales" department.

SELECT COUNT(*) AS TotalEmployees
FROM Employees e 
JOIN Departments d ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'

7.	Write a SQL query to find the number of all employees that have manager.

SELECT COUNT(ManagerID) AS Subordinates
FROM Employees 

8.	Write a SQL query to find the number of all employees that have no manager.

SELECT COUNT(*) AS SelfManaged
FROM Employees 
WHERE ManagerID IS NULL

9.	Write a SQL query to find all departments and the average salary for each of them.

SELECT d.Name, (SELECT ROUND(AVG(Salary),2)) AS AverageSalary
FROM Employees e JOIN Departments d
ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name

10.	Write a SQL query to find the count of all employees in each department and for each town.

SELECT COUNT(e.EmployeeID) AS [Employees Count], t.Name AS [Town], d.Name
FROM Employees e 
JOIN Departments d ON e.DepartmentID = d.DepartmentID
JOIN Addresses a ON e.AddressID = a.AddressID JOIN Towns t ON a.TownID = t.TownID
GROUP BY d.Name, t.Name

11.	Write a SQL query to find all managers that have exactly 5 employees. Display their first name and last name.

SELECT e.EmployeeID, e.FirstName, e.LastName, COUNT(e.EmployeeID)
FROM Employees e 
JOIN Employees m ON m.ManagerID = e.EmployeeID
GROUP BY e.EmployeeID, e.FirstName, e.LastName
HAVING COUNT(e.ManagerID) = 5

12.	Write a SQL query to find all employees along with their managers. For employees that do not have manager display the value "(no manager)".

SELECT CONCAT(e.FirstName, ' ', e.LastName) AS [Employee Name],
	   COALESCE(emp.FirstName + ' '+ emp.LastName, 'no manager') AS [Manager Name]
	FROM Employees e
	LEFT JOIN Employees emp
	ON e.ManagerID = emp.EmployeeID
	
13.	Write a SQL query to find the names of all employees whose last name is exactly 5 characters long. Use the built-in `LEN(str)` function.

SELECT LastName
FROM Employees
WHERE LEN(LastName) = 5

14.	Write a SQL query to display the current date and time in the following format "`day.month.year hour:minutes:seconds:milliseconds`".

SELECT FORMAT(GETDATE(), 'dd.MM.yyyy HH:mm:ss:fff')
	
15.	Write a SQL statement to create a table `Users`. Users should have username, password, full name and last login time.
	*	Choose appropriate data types for the table fields. Define a primary key column with a primary key constraint.
	*	Define the primary key column as identity to facilitate inserting records.
	*	Define unique constraint to avoid repeating usernames.
	*	Define a check constraint to ensure the password is at least 5 characters long.
	
CREATE TABLE Users (
UserId int IDENTITY PRIMARY KEY,
Username nvarchar(50) NOT NULL CHECK (LEN(Username) > 3) UNIQUE,
Pass nvarchar(20) CHECK (LEN(Password) > 5),
FullName nvarchar(50) NOT NULL CHECK (LEN(FullName) > 5),
LastLogin DATETIME)
	
16.	Write a SQL statement to create a view that displays the users from the `Users` table that have been in the system today.
	*	Test if the view works correctly.
	
	CREATE VIEW [Logged Users Today] AS 
	SELECT Username FROM Users
	WHERE DATEDIFF(DAY, LastLoginTime, GETDATE()) = 0
	
17.	Write a SQL statement to create a table `Groups`. Groups should have unique name (use unique constraint).
	*	Define primary key and identity column.
	
CREATE TABLE Groups(
GroupID int IDENTITY PRIMARY KEY,
name nvarchar(50) NOT NULL UNIQUE)
GO

18.	Write a SQL statement to add a column `GroupID` to the table `Users`.
	*	Fill some data in this new column and as well in the `Groups table.
	*	Write a SQL statement to add a foreign key constraint between tables `Users` and `Groups` tables.
	
ALTER TABLE Users
ADD GroupID int NOT NULL
GO

ALTER TABLE Users 
ADD CONSTRAINT GroupID FOREIGN KEY
(GroupID) REFERENCES Groups(GroupID)   
	
19.	Write SQL statements to insert several records in the `Users` and `Groups` tables.

INSERT INTO Groups
VALUES ('Excellent'), ('Very good'), ('Good'), ('Average'), ('Poor')

INSERT INTO Users(Username, Pass, FullName, LastLoginTime, GroupID)
VALUES ( 'asdnrhrettr', '123456', 'John Doe', '2015-10-14 00:00:00', 1)

20.	Write SQL statements to update some of the records in the `Users` and `Groups` tables.

UPDATE Users
SET FullName = UPPER(FullName)

UPDATE Groups
SET Name = UPPER(Name)

21.	Write SQL statements to delete some of the records from the `Users` and `Groups` tables.

DELETE FROM Groups
WHERE Name = 'POOR'

DELETE FROM Users
WHERE Pass = 123456

22.	Write SQL statements to insert in the `Users` table the names of all employees from the `Employees` table.
	*	Combine the first and last names as a full name.
	*	For username use the first letter of the first name + the last name (in lowercase).
	*	Use the same for the password, and `NULL` for last login time.
	
	INSERT INTO XmlCustomers.dbo.Users (Username, FullName, Pass)
        (SELECT LOWER(CONCAT(LEFT(FirstName, 3), LastName)), // have to change it to 3 and allow null values to the GroupID Foreign key, otherwise not executed
                CONCAT(FirstName, ' ', LastName),
                LOWER(CONCAT(FirstName, '.', LastName))
        FROM TelerikAcademy.dbo.Employees)
GO

23.	Write a SQL statement that changes the password to `NULL` for all users that have not been in the system since 10.03.2010.

UPDATE Users
SET Pass = NULL
WHERE LastLoginTime < DATEFROMPARTS(2015, 11, 10)

24.	Write a SQL statement that deletes all users without passwords (`NULL` password).

DELETE Users
WHERE Pass IS NULL

25.	Write a SQL query to display the average employee salary by department and job title.

SELECT ROUND(AVG(Salary),2), d.Name AS DepartmentName, e.JobTitle
FROM Employees e, Departments d
GROUP BY d.Name, e.JobTitle

26.	Write a SQL query to display the minimal employee salary by department and job title along with the name of some of the employees that take it.

SELECT ROUND(MIN(Salary),2), d.Name AS DepartmentName, e.JobTitle , 
	(MIN(CONCAT(e.FirstName, ' ', e.LastName))) AS FullName
FROM Employees e, Departments d
GROUP BY d.Name, e.JobTitle

27.	Write a SQL query to display the town where maximal number of employees work.

SELECT TOP 1 t.Name, COUNT(e.EmployeeID)
FROM Employees e 
JOIN Addresses a ON e.AddressID = a.AddressID
JOIN Towns t ON t.TownID = a.TownID
GROUP BY t.Name

28.	Write a SQL query to display the number of managers from each town.

SELECT t.Name AS [Town], COUNT(DISTINCT e.ManagerID) AS [Number of managers]
FROM Employees e
	JOIN Employees m
		ON e.ManagerID = m.ManagerID
	JOIN Addresses a
		ON m.AddressID = a.AddressID
	JOIN Towns t
		ON a.TownID = t.TownID
GROUP BY t.Name

29.	Write a SQL to create table `WorkHours` to store work reports for each employee (employee id, date, task, hours, comments).
	*	Don't forget to define  identity, primary key and appropriate foreign key. 
	*	Issue few SQL statements to insert, update and delete of some data in the table.
	*	Define a table `WorkHoursLogs` to track all changes in the `WorkHours` table with triggers.
		*	For each change keep the old record data, the new record data and the command (insert / update / delete).
		
CREATE TABLE Tasks(
TaskId int PRIMARY KEY,
Task nvarchar(200),
WorkHours NUMERIC
CHECK (WorkHours >  0)
)	

CREATE TABLE WorkHours(
EmployeeId int PRIMARY KEY NOT NULL,
TaskId int,
FOREIGN KEY (TaskId) REFERENCES Tasks (TaskID),
Date DATETIME NOT NULL,
Hours NUMERIC,
Comments nvarchar(250) 
)

INSERT INTO Tasks(TaskId, Task, WorkHours)
VALUES(1, 'Get some sleep', 12)	

INSERT INTO Tasks(TaskId, Task, WorkHours)
VALUES(2, 'Book flights for holiday', 1)

UPDATE Tasks
SET WorkHours = 14
WHERE Task = 'Get some sleep'

DELETE FROM Tasks
WHERE TaskId = 2
		
30.	Start a database transaction, delete all employees from the '`Sales`' department along with all dependent records from the pother tables.
	*	At the end rollback the transaction.
	
	BEGIN TRAN
	ALTER TABLE Departments
	DROP CONSTRAINT FK_Departments_Employees

	ALTER TABLE WorkHours
	DROP CONSTRAINT FK_WorkHours_Employees

	DELETE FROM Employees
	SELECT d.Name
	FROM Employees e
		JOIN Departments d
			ON e.DepartmentID = d.DepartmentID
	WHERE d.Name = 'Sales'
	GROUP BY d.Name
ROLLBACK TRAN

31.	Start a database transaction and drop the table `EmployeesProjects`.
	*	Now how you could restore back the lost table data?
	
32.	Find how to use temporary tables in SQL Server.
	*	Using temporary tables backup all records from `EmployeesProjects` and restore them back after dropping and re-creating the table.
	CREATE TABLE #TemporaryTable (
	EmployeeId INT,
	ProjectId INT
)

INSERT INTO #TemporaryTable
SELECT EmployeeId, ProjectId
FROM EmployeesProjects

DROP TABLE EmployeesProjects

CREATE TABLE EmployeesProjects (
	EmployeeId INT,
	ProjectId INT,
	CONSTRAINT PK_EmployeesProjects PRIMARY KEY(EmployeeID, ProjectID),
	CONSTRAINT FK_EmployeesProjects_Employees FOREIGN KEY(EmployeeID) 
	REFERENCES Employees(EmployeeID),
	CONSTRAINT FK_EmployeesProjects_Projects FOREIGN KEY(ProjectID) 
	REFERENCES Projects(ProjectID)
)

INSERT INTO EmployeesProjects
SELECT EmployeeId, ProjectId
FROM #TemporaryTable