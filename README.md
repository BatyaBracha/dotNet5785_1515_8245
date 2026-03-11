# Emergency Response Management System (ERMS)
## "We save lives"

### 🚀 Overview
**ERMS** is a mission-critical desktop application engineered to coordinate emergency volunteer workflows. Designed for high-pressure environments, the system ensures that every second counts by optimizing the process of logging distress calls, dispatching volunteers, and monitoring response status in real-time.

### 🏗️ Architectural Excellence
The system is built with a sophisticated **N-Tier Architecture**, ensuring a professional separation of concerns:
* **Presentation Layer (PL):** A reactive WPF interface utilizing the **Observer Pattern** to provide live system updates (such as call counters and system clock) without manual refreshing.
* **Business Logic Layer (BL):** A robust core that handles complex assignment logic, role-based security, and a built-in **Simulation Engine** for stress-testing response times.
* **Data Access Layer (DAL):** Implements the **Factory Pattern** to seamlessly manage data persistence via **XML** or in-memory lists, ensuring data integrity at all times.

### ✨ Key Engineering Features
* **Real-time Temporal Simulator:** Allows administrators to accelerate system time to analyze long-term volunteer patterns and bottlenecks.
* **Advanced Data Modeling:** Extensive use of **LINQ** and asynchronous operations to ensure a smooth, high-performance user experience even under heavy loads.
* **Enterprise Security:** Secured access control with role-based permissions (Admin/Volunteer) and asynchronous password management.
* **Maintainable Design:** Adheres to **MVVM** and **SOLID principles**, making the codebase easy to extend and scale.

---

### 👤 Contributors
* **Batsheva Malka Rechnitzer** – *Full-Stack Developer*
* **Batya Bracha Oren** – *Full-Stack Developer*

### ⚙️ Setup & Deployment
1. Open the solution file in Visual Studio.
2. Ensure **PL** is set as the Startup Project.
3. Build and run the application.

*Default Admin Credentials:* **User:** `328118245` | **Pass:** `B7malka!`
