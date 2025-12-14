# Hotel Maintenance Order Management System

A comprehensive web application for managing hotel maintenance orders across multiple properties using ASP.NET Core 8 MVC.

## Features

### 1. Order Management
- Create, track, and manage maintenance orders
- Order lifecycle with 9 statuses: Created, Awaiting Assignment, Assigned, Sent to Contractor, Scheduled, Completed, Finalized, Canceled, Unsolved
- Assignment to departments and employees with audit trail
- Priority-based task management (Low, Medium, High, Urgent)
- Expected completion date tracking
- Support for guest requests and internal maintenance
- Image upload for order documentation

### 2. Order Tracking
- View active orders by department, hotel, or status
- Complete order history with status changes
- Priority and SLA monitoring
- Real-time notifications via SignalR
- Filter orders by date range, hotel, department, and status

### 3. Item Management
- Comprehensive inventory of equipment and items
- Categorization by category, class, and family
- Track item status (Active, Broken, Maintenance)
- Location tracking across hotels
- Warranty and manufacturer information
- Image support for items

### 4. Spare Parts Management
- Track spare parts inventory
- Associate parts with items and departments
- Quantity tracking with reorder thresholds
- Cost tracking per part

### 5. Multi-Hotel Support
- Manage multiple hotel properties
- Hotel-specific orders and items
- Centralized management dashboard

### 6. Notification System
- Real-time notifications using SignalR
- Toast-style notifications for better UX
- Automatic reconnection handling

### 7. Reporting & Export
- Export orders to Excel format
- Export orders to PDF format
- Dashboard with analytics and statistics
- Visual charts for order status and priority distribution

### 8. Authentication & Authorization
- Role-based access control
- User roles: Admin, Manager, Technician, FrontDesk, Housekeeping
- Secure login and registration
- Access denied handling

### 9. Localization
- Multi-language support (English and Arabic)
- Localized resources for UI elements

## Technology Stack

- **Framework**: ASP.NET Core 8 MVC
- **Database**: SQL Server with Entity Framework Core
- **Real-time Communication**: SignalR
- **Authentication**: ASP.NET Core Identity
- **UI Framework**: Bootstrap 5
- **Charts**: Chart.js
- **Export**: ClosedXML (Excel), PdfSharpCore (PDF)

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- SQL Server (LocalDB, Express, or Full)
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository
```bash
git clone https://github.com/aibrahiiim1/HotelMaintenanceSystem.git
cd HotelMaintenanceSystem
```

2. Update the connection string in `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HotelMaintenance;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

3. Apply database migrations
```bash
cd HotelMaintenanceSystem
dotnet ef database update
```

4. Run the application
```bash
dotnet run
```

5. Open your browser and navigate to `https://localhost:5001`

### Default Credentials
The application seeds a default admin user:
- **Email**: admin@hotel.com
- **Password**: Admin@123

### Sample Data
The application automatically seeds sample data including:
- 3 sample hotels
- Sample maintenance items
- Sample spare parts
- Sample maintenance orders
- Department data (Housekeeping, Engineering, Security, Laundry, Front Office)
- Order statuses

## Project Structure

```
HotelMaintenanceSystem/
├── Controllers/          # MVC Controllers
│   ├── AccountController.cs
│   ├── DashboardController.cs
│   ├── ExportController.cs
│   ├── HomeController.cs
│   ├── HotelsController.cs
│   ├── ItemsController.cs
│   ├── OrdersController.cs
│   └── SparePartsController.cs
├── Data/                 # Database context and initializer
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs
├── Hubs/                 # SignalR hubs
│   └── NotificationHub.cs
├── Migrations/           # EF Core migrations
├── Models/               # Domain models
│   ├── Department.cs
│   ├── Hotel.cs
│   ├── Item.cs
│   ├── LoginViewModel.cs
│   ├── Order.cs
│   ├── OrderHistory.cs
│   ├── OrderStatus.cs
│   ├── RegisterViewModel.cs
│   └── SparePart.cs
├── Resources/            # Localization resources
│   ├── SharedResource.cs
│   ├── SharedResource.en-US.resx
│   └── SharedResource.ar-SA.resx
├── Services/             # Business services
│   ├── ExportService.cs
│   └── NotificationService.cs
├── Views/                # Razor views
│   ├── Account/
│   ├── Dashboard/
│   ├── Home/
│   ├── Hotels/
│   ├── Items/
│   ├── Orders/
│   ├── Shared/
│   └── SpareParts/
└── wwwroot/              # Static files
    ├── css/
    ├── js/
    └── lib/
```

## Key Features Implementation

### Real-time Notifications
The system uses SignalR for real-time notifications. When an order status changes or new orders are created, all connected users receive instant notifications via toast messages.

### Order Lifecycle
Orders go through a comprehensive lifecycle:
1. **Created** - Initial creation
2. **Awaiting Assignment** - Ready for assignment
3. **Assigned** - Assigned to a technician
4. **Sent to Contractor** - Escalated to external contractor
5. **Scheduled** - Work scheduled
6. **Completed** - Work finished
7. **Finalized** - Verified and closed
8. **Canceled** - Order canceled
9. **Unsolved** - Unable to resolve

### Dashboard Analytics
The dashboard provides:
- Total, completed, and pending order counts
- Order status distribution (doughnut chart)
- Priority distribution (bar chart)
- Quick action buttons

## Security Features

- Password hashing with ASP.NET Core Identity
- Role-based authorization
- Anti-forgery tokens for forms
- Secure authentication cookies
- SQL injection prevention via EF Core

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License.

## Support

For issues, questions, or contributions, please open an issue on GitHub.
