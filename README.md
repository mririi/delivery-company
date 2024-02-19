# Delivery Company Management

## Startup
### Step 1 :
Add the query in deliveryCompany.sql to the database
### Step 2 :
Add these 3 Packages via NuGet manager (from tools -> Nuget Package Manager -> Manage NuGet packages for Solution if you are using VS)
### Step 3 :
Execute the commande in NuGet Console to generate Models based on the database you have : <br>
``Scaffold-DbContext "Server=(localdb)\ProjectModels;Database=deliveryCompany;Trusted_Connection=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models``
