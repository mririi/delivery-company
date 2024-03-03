-- Create Users table
CREATE TABLE Users (
    user_id INT PRIMARY KEY,
    user_type INT NOT NULL, -- 0 for 'Administrator', 1 for 'Driver'
    user_name VARCHAR(50) NOT NULL,
    pass VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    phone_number VARCHAR(15),
    UNIQUE (user_name),
    UNIQUE (email)
);

-- Create Vehicles table
CREATE TABLE Vehicles (
    vehicle_id INT PRIMARY KEY,
    driver_id INT,
    vehicle_number VARCHAR(20) NOT NULL,
    model VARCHAR(50) NOT NULL,
    vehicle_status VARCHAR(20) DEFAULT 'Available',
    year_model INT,
    FOREIGN KEY (driver_id) REFERENCES Users(user_id)
);

-- Create Maintenance table
CREATE TABLE Maintenance (
    maintenance_id INT PRIMARY KEY,
    vehicle_id INT,
    maintenance_date DATE NOT NULL,
    description_maintenance VARCHAR(255),
    cost DECIMAL(10, 2),
    FOREIGN KEY (vehicle_id) REFERENCES Vehicles(vehicle_id)
);

-- Create Orders table
CREATE TABLE Orders (
    order_id INT PRIMARY KEY,
    customer_id INT,
    delivery_address VARCHAR(100) NOT NULL,
    order_date DATE NOT NULL,
    price float,
    order_status VARCHAR(20) DEFAULT 'Pending', -- Can be 'Pending', 'In Progress', 'Delivered', Returned.
    assigned_driver_id INT,
    FOREIGN KEY (assigned_driver_id) REFERENCES Users(user_id),
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)
);

CREATE TABLE Customers (
    customer_id INT PRIMARY KEY,
    customer_name VARCHAR(50) NOT NULL,
    customer_email VARCHAR(50) NOT NULL,
    customer_address VARCHAR(50),
    customer_phone varchar(15),
)

-- Create DriverOrders table (junction table for many-to-many relationship)
CREATE TABLE DriverOrders (
    driver_id INT,
    order_id INT,
    PRIMARY KEY (driver_id, order_id),
    FOREIGN KEY (driver_id) REFERENCES Users(user_id),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id)
);

-- Sample data for Users table
INSERT INTO Users (user_id, user_type, user_name, pass, email, phone_number)
VALUES
(1, 0, 'admin', 'admin123', 'admin@example.com', NULL),
(2, 1, 'driver1', 'driverpass', 'driver1@example.com', '123-456-7890'),
(3, 1, 'driver2', 'driverpass', 'driver2@example.com', '987-654-3210');

-- Sample data for Vehicles table
INSERT INTO Vehicles (vehicle_id, driver_id, vehicle_number, model, vehicle_status, year_model)
VALUES
(1, 2, 'ABC123', 'Car Model 1', 'Available', 2020),
(2, 3, 'XYZ789', 'Truck Model 2', 'Available', 2019),
(3, NULL, 'DEF456', 'Bike Model 3', 'Unavailable', 2022);

-- Sample data for Maintenance table
INSERT INTO Maintenance (maintenance_id, vehicle_id, maintenance_date, description_maintenance, cost)
VALUES
(1, 1, '2023-01-15', 'Regular checkup', 50.00),
(2, 2, '2023-02-20', 'Oil change', 80.00),
(3, 3, '2023-03-25', 'Tire replacement', 120.00);

-- Sample data for Customers table
INSERT INTO Customers (customer_id, customer_name, customer_email, customer_address, customer_phone)
VALUES
(1, 'John Doe', 'john.doe@example.com', '123 Main St', '555-1234'),
(2, 'Jane Smith', 'jane.smith@example.com', '456 Oak Ave', '555-5678');

-- Sample data for Orders table
INSERT INTO Orders (order_id, customer_id, delivery_address, order_date, price, order_status, assigned_driver_id)
VALUES
(1, 1, '789 Elm St', '2023-04-01', 75.00, 'Pending', 2),
(2, 2, '567 Pine Ln', '2023-04-02', 120.00, 'In Progress', 3),
(3, 1, '456 Birch Rd', '2023-04-03', 50.00, 'Delivered', 2);

-- Sample data for DriverOrders table
INSERT INTO DriverOrders (driver_id, order_id)
VALUES
(2, 1),
(3, 2),
(2, 3);
