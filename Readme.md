E-Commerce Marketplace Documentation (Product/Technical)
==========================================
<pre>
  The E-Commerce Marketplace App is an application that allows users to buy and sell products.
  You as user have the options to become a vendor, add new products, buy products, view your bought item history,
  view your current orders, search for products by name, category and status (stocked/unavailable).
  
  I hope you enjoy using the app and reading the code!
  Now let's see the app documentation. 
</pre>

Product Documentation
==========================================
### Application Flow
    When you start the app you are redirected to the homepage.
    There you'll see a navbar with several buttons and also a list of all the available products.
    You can see the details of each product individually.
  <p>
    <img height="300em" src="https://i.ibb.co/60wksJr/homepage.png"></img>
  </p>
    <pre>
    The buttons on the the navbar are:
    1. On the far left we have the "All Products" button which displays all available products in the store.
    2. On the far right we have the "Register" button which lets you register a new account.
    3. The "Login" button next to the "Register" button lets you log into the site if you have an existing account.
    </pre>
    <p>
      <img src="https://i.ibb.co/NN6xTtF/navbar.png"></img>
    </p>
    <pre>
      The login form asks for your email address and password. 
      If by any chance you forgot your password you can click the forgot password link and 
      reset it with a reset password mail.
      There is also a link which takes you to the register form if you don't have an account and
      a link which resends the confirmation email for your account.
    </pre>
    <p>
      <img height="300em" src="https://i.ibb.co/Km29fZV/login.png"></img>
      <img height="300em" src="https://i.ibb.co/k8hyDDD/passforget.png"></img>
      <img height="300em" src="https://i.ibb.co/HBTqfKK/resendmail.png"></img>
    </p>
    <pre>
    The register form asks for:
      - Email Address
      - Password
      - Confirm Password
    </pre>
    <img height="350em" src="https://i.ibb.co/1JMVLLC/register.png"></img>
    <pre>
       If you click on a "Details" button of a product, you are redirected to a page that displays
       the product information and the option to Buy it.
    </pre>
    <img height="350em" src="https://i.ibb.co/Q8gnzc1/Product-Details.png"></img>
    <pre>
      After you have logged in as a user, the navbar receives several new buttons:
        - Orders
        - History
        - Become Vendor
        - Account Management button (your e-mail address)
        - Logout
    </pre>
    <img src="https://i.ibb.co/Stb5Fvb/logged-User-Navbar.png"></img>
    <pre>
     The "Orders" button shows you all your items currently being processed.
    </pre>
    <img src="https://i.ibb.co/BjdRWMX/Orders.png"></img>
    <pre>
     The "History" button shows you all your bought items so far.
    </pre>
    <img src="https://i.ibb.co/z70cb74/History.png"></img>
    <pre>
      The Become a Vendor form, accessed by clicking the "Become Vendor" button,
      asks you if you would like to upload products and sell them through the store.
      It requires:
        - Your First Name
        - Your Last Name
        - Your Phone Number
    </pre>
    <img src="https://i.ibb.co/smXB6NW/become-Vendor.png"></img>
    <pre>
     The Account Management button redirects you to a page where you can change the settings of your account:
        - Your profile information (Username and Phone Number)
        - Your e-mail
        - Password
        - Option of adding a two-factor authentication app
        - Section where you can download or delete your personal data
    </pre>
    <img src="https://i.ibb.co/bm6BkJp/Act-Management.png"></img>
    <pre>
     After becoming a vendor, you also receive two additional buttons in the navbar:
        - Add Product
        - My Products
    </pre>
    <img src="https://i.ibb.co/0MFFDzs/add-Buttons.png"></img>
    <pre>
      The "Add Product" button redirects you to a page where you can add a product you want to sell by using the form provided.
      The product details that you must enter are:
        - Product Name
        - Price
        - Image URL
        - Category
        - Description
    </pre>
    <img src="https://i.ibb.co/X4XphLK/add-Product.png"></img>
    <pre>
     By clicking the "My Products" button you can see all your added products that you're currently selling.
    </pre>
    <img src="https://i.ibb.co/82f280X/my-Products.png"></img>
    <pre>
      When you log as admin you're being redirected to the store dashboard page.
      At first glance the admin sees:
        1. "All Products" button leading to a page containing all the products currently in the store.
        2. "Add Product" button which redirects you to the "Become Vendor" registration form (if you have not registered as a vendor).
        3. "My Products" button which redirects you to a page that shows you your added (if you're a vendor) and bought products.
        4. "All Users" button which shows you a table with all the users and vendors.
    </pre>
    <img src="https://i.ibb.co/X2TwsJp/admin.png"></img>
    <pre>
      In the "All Users" section you have the option of viewing the user's:
        - E-mail
        - Full Name
        - Phone Number
        - If the user is a vendor
      You can also delete a user by clicking the "Forget" button.
    </pre>
    <img src="https://i.ibb.co/DVLh6ZN/allUsers.png"></img>
    
Technical Documentation
=====================================
### Brief information
    The application uses SqlServer DbContext + Identity for the user control.
    The application uses POCO classes for the Configuration root json's.
    The application uses JWT Bearer scheme and custom refresh tokens for the authentication/authorization.
    The application uses Repository pattern for getting data from the database.
    The application uses Swagger in Development Environment for the API documentation.
    The application uses Email confirmation registration.

### Application setup on local machine
    Step 1. Clone the repo and open the Store-Ge.Web.sln project file in the Store-Ge.Web folder
    Step 2. Initialize user secrets for the project
      The Keys needed for the project to work properly are:
      -"SendGridSettings:SendGridApiKey"
      -"JwtSettings:Secret"
      -"DbUser"
      -"DbPassword"
      -"DbConfiguration:ConnectionString"
      -"StoreGeAppSettings:DataProtectionKey"
    Step 3. Register in https://sendgrid.com/ with free account and get the API key that's been given to you(don't give it to anyone else). Add the key to the "SendGridSettings:SendGridApiKey" secret
    Step 4. The other secrets are up to you to give them random suitable value(strong highly unguessable values are recommended)
    Step 5. If you run your SQL Server on Docker start it.
    Step 6. Open the Store-Ge.UI project with VS Code and run the "ng serve -o" command in the terminal. This will serve the front end project and will be opened on port 4200.
    Step 7. Start the Web project under the IIS Express profile
    Step 8. Should be ready to use!
    
### Application Flow
<p>
    <pre>                    - Registration Flow                                     - Functionality</pre>
    <p>
      <img height="400em" src="https://i.ibb.co/LCGWh18/store-ge-registration-flow.jpg"/></img>
      <img height="600em" src="https://i.ibb.co/WpMJJ1t/store-ge-component-diagram.jpg"/></img>
    </p>
</p>

Tech Stack:
==========================================

### API
<p></p>
<ul>
  <li>ASP.Net Core 6.0</li>
  <li>EntityFramework Core 6.0.1</li>
  <li>Z.EntityFramework.Extensions.EFCore 6.16.1</li>
  <li>AutoMapper 12.0</li>
  <li>LinqKit 1.2.2</li>
  <li>SendGrid 9.28.1</li>
  <li>EPPlus 6.1.1</li>
  <li>Swashbuckle.AspNetCore.Swagger 6.4</li>
  <li>Microsoft.AspNetCore.Identity 6.0.1</li>
  <li>Microsoft.AspNetCore.Authentication.JwtBearer 6.0.9</li>
</ul>

### Front-End
<p></p>
<ul>
  <li>Angular 14.2.6</li>
  <li>Material 13.3.9</li>
  <li>RxJs 7.5.7</li>
  <li>TypeScript 4.6.4</li>
  <li>JwtHelper</li>
</ul>

### Database
<p></p>
<ul>
  <li>MSSQL Server</li>
</ul>

### Tests
<p></p>
<ul>
  <li>NUnit 3.13.3</li>
  <li>NUnit3TestAdapter 4.3.1</li>
  <li>Moq 4.18.2</li>
  <li>Microsoft.EntityFrameworkCore.InMemory 6.0.11</li>
  <li>Microsoft.NET.Test.Sdk 17.4</li>
  <li>coverlet.collector 3.2</li>
</ul>

### Git tools
<p></p>
<ul>
  <li>GitHub</li>
  <li>GitHub Desktop/Tortoise Git</li>
</ul>