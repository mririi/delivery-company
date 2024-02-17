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
    order_status VARCHAR(20) DEFAULT 'Pending', -- Can be 'Pending', 'In Progress', 'Delivered', Returned.
    assigned_driver_id INT,
    FOREIGN KEY (assigned_driver_id) REFERENCES Users(user_id),
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)
);

CREATE TABLE Customers (
    customer_id INT PRIMARY KEY,
    customer_name VARCHAR(50) NOT NULL,
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
    (1, 0, 'admin', 'admin123', 'admin@example.com', '123-456-7890'),
    (2, 1, 'driver1', 'driver123', 'driver1@example.com', '987-654-3210'),
    (3, 1, 'driver2', 'driver456', 'driver2@example.com', '111-222-3333');

-- Sample data for Vehicles table
INSERT INTO Vehicles (vehicle_id, driver_id, vehicle_number, model, vehicle_status, year_model)
VALUES
    (101, 2, 'ABC123', 'Truck', 'Available', 2020),
    (102, 3, 'XYZ789', 'Van', 'Available', 2019);

-- Sample data for Maintenance table
INSERT INTO Maintenance (maintenance_id, vehicle_id, maintenance_date, description_maintenance, cost)
VALUES
    (201, 101, '2024-01-15', 'Oil Change', 50.00),
    (202, 102, '2024-02-01', 'Brake Inspection', 75.50);

-- Sample data for Customers table
INSERT INTO Customers (customer_id, customer_name)
VALUES
    (301, 'Customer A'),
    (302, 'Customer B');

-- Sample data for Orders table
INSERT INTO Orders (order_id, customer_id, delivery_address, order_date, order_status)
VALUES
    (401, 301, '123 Main St, City A', '2024-02-13', 'Pending'),
    (402, 302, '456 Oak St, City B', '2024-02-14', 'In Progress');

-- Sample data for DriverOrders table
INSERT INTO DriverOrders (driver_id, order_id)
VALUES
    (2, 401),
    (3, 402);
