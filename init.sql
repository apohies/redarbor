CREATE DATABASE IF NOT EXISTS redarbordb;
USE redarbordb;

CREATE TABLE IF NOT EXISTS Employee (
    Id          INT AUTO_INCREMENT PRIMARY KEY,
    CompanyId   INT NOT NULL,
    CreatedOn   DATETIME NULL,
    DeletedOn   DATETIME NULL,
    Email       VARCHAR(255) NOT NULL,
    Fax         VARCHAR(50) NULL,
    Name        VARCHAR(255) NULL,
    Lastlogin   DATETIME NULL,
    Password    VARCHAR(255) NOT NULL,
    PortalId    INT NOT NULL,
    RoleId      INT NOT NULL,
    StatusId    INT NOT NULL,
    Telephone   VARCHAR(50) NULL,
    UpdatedOn   DATETIME NULL,
    Username    VARCHAR(100) NOT NULL
);
