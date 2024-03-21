# Datapac - Library API assignment

## Prerequisities and notes:

 - Webapi project is configured to use default configuration of SQL Express DB - feel free to update connection string to match your environment.
 - DB for library is using "code first" approach so in case you haven't created one it gets created during DbContext initialization.
 - Project uses hangfire for scheduling tasks. That means hangfire uses its own DB connection which means it is required to create empty database before first use, everything else (tables) will be created automatically.

I have used simple db names **datapac** and **hangfire** for development purposes.

   ```json
 "ConnectionStrings": {
   "DefaultConnection": "Server=localhost\\SQLEXPRESS01;Database=datapac;Trusted_Connection=True;TrustServerCertificate=True;",
   "HangfireConnection": "Server=localhost\\SQLEXPRESS01;Database=hangfire;Trusted_Connection=True;TrustServerCertificate=True;"
 }
```

 - Hangfire is manageable via it's dedicated endpoint - https://localhost:port/hangfire where all scheduled tasks could be managed.

## Library work-flow

 1. User borrows book from Library. 
 2. From now on he has 7 days to return it back.
 3. After 6 days notification email will be sent.
 4. If he does return during this period, notification wont be sent.
 5. If he returns the book after this period, he gets blocked - he wont be able to borrow any book in the future.
 6. If user wants to borrow a book, there is always a check whether there is some pending notification and whether it is expired or not. In case it is already expired (After 7 days), user gets blocked.

## Unit Testing:

 - I did some basic unit testing just for demonstration which mocking tool I use the most and that is FakeItEasy. I know it has some issues but still my favourite among all the providers out there.