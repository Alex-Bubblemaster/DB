## Database Systems - Overview
### _Homework_

#### Answer following questions in Markdown format (`.md` file)

1.  What database models do you know?

There are many database models.
  * Hierarchical database model
    created by IBM in the 60s - it has a tree like structure where each child can only have one parent and the whole tree        needs to be traversed 
  * Network model
    It is a flexible way of representin objects and their relationships. The object types are nodes represented in a graph 
  * Relational model
    Data is organised in tuples(tables) - grouping any number of values together into a single compound value.
    They provide a declarative method for specifying data and queries.
    Entity–relationship model
    Describes the relationship between data and it's dependencies in an abstract way. It is very limiting as represents only     1 out of 14 possible models.
    Replaced by tools that enable diagramming within relational DB Management Systems.
  * Enhanced entity–relationship model
    The EER(Enhanced Entity Relationship) is closely related to OOP.
    It builds on the ER model by adding super and sub- classes, specialization and generalization.
  * Object model
  * Document model
  * Entity–attribute–value model
  * Star schema
  
2.  Which are the main functions performed by a Relational Database Management System (RDBMS)?

  * RDBMSs are a common choice for the storage of information in new databases used for financial records, manufacturing and     logistical information, personnel data, and other applications since the 1980s.
  * RDBMSs manage the organization, storage, access, security and integrity of data. 
  
3.  Define what is "table" in database terms.

  * It is a DB object - a collection of related data entries and it consists of columns and rows.Columns have name and type.     All rows have the same structure
  
4.  Explain the difference between a primary and a foreign key.

  * Foreign key is a column in one table which is primary key on another table. Foreign key and Primary key is used to define     relationship between two tables in relational database. 
  
5.  Explain the different kinds of relationships between tables in relational databases.
  
  * one-to-one
  
   Both tables can have only one record on either side of the relationship. Each primary key value relates to only one (or      no) record in the related table. They're like spouses—you may or may not be married, but if you are, both you and your       spouse have only one spouse.

  * one-to-many
  
   The primary key table contains only one record that relates to none, one, or many records in the related table. This         relationship is similar to the one between you and a parent. You have only one mother, but your mother may have several      children.

  * many-to-many
  
   Each record in both tables can relate to any number of records (or no records) in the other table. For instance, if you      have several siblings, so do your siblings (have many siblings). Many-to-many relationships require a third table, known     as an associate or linking table, because relational systems can't directly accommodate the relationship.

6.  When is a certain database schema normalized? What are the advantages of normalized databases?

    Database Normalisation is a technique of organizing the data in the database. Normalization is a systematic approach of      decomposing tables to eliminate data redundancy and undesirable characteristics like Insertion, Update and Deletion          Anamolies. It is a multi-step process that puts data into tabular form by removing duplicated data from the relation         tables.

    Normalization is used for mainly two purposes:

  * Eliminating reduntant(useless) data.
  * Ensuring data dependencies make sense i.e data is logically stored.
  
7.  What are database integrity constraints and when are they used?

  * Integrity constraints are used to ensure accuracy and consistency of data in a relational database. It has to do with        data validity.
    Data integrity is handled in a relational database through the concept of referential integrity. 
     * Primary Key Constraints
     * Unique Constraints
     * Foreign Key Constraints
     * NOT NULL Constraints
     * Check Constraints

8.  Point out the pros and cons of using indexes in a database.

  * indices speed up searching for values in a certain column or group of columns
  * greatest con is that insertion and deletion of records is slower as it requires same update to each indexed file. Also it     takes up space to be stored
  
9.  What's the main purpose of the SQL language?

    Structured Query Language is a special-purpose programming language designed for managing data held in a relational          database management system (RDBMS), or for stream processing in a relational data stream management system (RDSMS).

10.  What are transactions used for?
  
  Transactions are units or sequences of work accomplished in a logical order, whether in a manual fashion by a user or        automatically by some sort of a database program.
  All operations are executed as a single unit. If one fails, all fail. They are used to ensure data integrity. Transaction    is implemented in database using SQL keyword transaction, commit and rollback. Commit writes the changes made by             transaction into database and rollback removes temporary changes logged in transaction log by database transaction.

  * Give an example.
  ATM Withdrawal usually goes like this:

  * Verify account details.
  * Accept withdrawal request
  * Check balance
  * Update balance
  * Dispense money

start transaction
select balance from Account where Account_Number='9001';
select balance from Account where Account_Number='9002';
update Account set balance=balance-900 here Account_Number='9001' ;
update Account set balance=balance+900 here Account_Number='9002' ;
commit; //if all sql queries succed
rollback; //if any of Sql queries failed or error

11.  What is a NoSQL database?

   A NoSQL (originally referring to "non SQL" or "non relational") database provides a mechanism for storage and retrieval of    data that is modeled in means other than the tabular relations used in relational databases.
   he data structures used by NoSQL databases (e.g. key-value, graph, or document) differ slightly from those used by default    in relational databases, making some operations faster in NoSQL and others faster in relational databases. 

12.  Explain the classical non-relational data models.
13.  Give few examples of NoSQL databases and their pros and cons.

    Document databases pair each key with a complex data structure known as a document. Documents can contain many different     key-value pairs, or key-array pairs, or even nested documents.
    
  * Graph stores are used to store information about networks, such as social connections. Graph stores include Neo4J and        HyperGraphDB.
  * Key-value stores are the simplest NoSQL databases. Every single item in the database is stored as an attribute name (or     "key"), together with its value. Examples of key-value stores are Riak and Voldemort. Some key-value stores, such as          Redis, allow each value to have a type, such as "integer", which adds functionality.
  * Wide-column stores such as Cassandra and HBase are optimized for queries over large datasets, and store columns of data      together, instead of rows.
   * Data Storage model varies based on database type. For example, key-value stores function similarly to SQL databases, but     have only two columns ("key" and "value"), with more complex information sometimes stored within the "value" columns.        Document databases do away with the table-and-row model altogether, storing all relevant data together in single             "document" in JSON, XML, or another format, which can nest values hierarchically.
