# Changelog

## [2.0.0] - 2026-03-01

### Added
- 50 professional improvements implemented
- PBKDF2 password hashing with random salt (100K iterations)
- 15-minute session timeout with audit trail
- Login lockout after 3 failed attempts (5-min cooldown)
- AES encryption support for JSON data files
- Input sanitization for CSV injection prevention
- Role-based access control (Admin/Warden/Accountant/Staff)
- Data seeding with 10 students, 15 rooms, 5 staff, menus
- Defaulter list with outstanding amount summary
- Late fee calculation with grace period
- Auto room allocation algorithm with scoring
- Student check-in/check-out tracking
- Leave management system (request/approve/reject)
- Inventory management (furniture, electronics, keys)
- Notification service with type-based alerts
- Backup & Restore system with timestamped backups
- File-based logging (daily log files)
- Configuration file (appsettings.json)
- Pagination support for large datasets
- File locking for concurrent access safety
- Schema versioning for data migration
- Soft delete pattern on new entities
- Theme switching (Dark/Light/Blue)
- Help system with context-sensitive guides
- Screen size detection for responsive tables
- PDF-style formatted report exports
- Trend analysis (month-over-month revenue)
- Occupancy forecasting with predictions
- Plugin system interface (IHostelPlugin)
- Auto-update checker stub
- 16 unit tests (100% passing)
- Docker support (Dockerfile)
- CI/CD pipeline (GitHub Actions)
- Solution file (HostelManagement.sln)
- VERSION and CHANGELOG files
- MIT License

### Removed
- Old EfCore.cs, Data.cs, InMemory.cs files
- Plain SHA256 password hashing (replaced with PBKDF2)

## [1.0.0] - 2026-02-27
### Initial Release
- Basic console app with 4 menu options
- Student, Room, Booking, Payment, Complaint entities
- In-memory data storage
