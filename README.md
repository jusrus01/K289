# K289 team
## Jira workflow idea
1. Before starting a sprint, review how user stories are formulated and make required changes.
2. Try to assign front-end developer and back-end developer to the same story, to avoid full stack work.
3. Create subtasks for selected story to see progress of the back-end and front-end.
4. Decide on the contract between two people, e.g. the end-point that will be created and what kind of json object it will return. Furthermore, front-end developer should work with mocked data, if the back-end is not available yet. Moreover, front-end should be merged to the project using the real end-point even if back-end is not yet finished (unless the back-end developer will not be able to finish his work during the sprint).

# Back-end notes
## Easy way to run it

I recommend using Visual Studio 2022 for this project, since it was created using this version.
Older versions might work fine though.

Steps:
1. This project uses Microsoft SQL Server as a database provider. 
If you are not sure how to set up a database just download 
[SSMS](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) and you will be fine.
Before running the project you should [create a database](https://support.mailessentials.gfi.com/hc/en-us/articles/360015116400-How-to-create-a-new-database-in-Microsoft-SQL-Server) named **TourneyRentDatabase**
(if you intend to use default connection string defined in appsettings.json e.g.
```Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=60; MultipleActiveResultSets=True;Database=TourneyRentDatabase;```)

2. Next step would be restoring missing nuget packages. You can find some information about it [here](https://docs.microsoft.com/en-us/nuget/consume-packages/package-restore).
3. If at this point the solution compiles, you will need to do one more thing. Run command ```Update-Database -Project TourneyRent.DataLayer -StartUpProject TourneyRent.Presentation.Api```
in Package Manager Console (you can find it in VStudio menu by navigating to Tools > NuGet Package Manager > Package Manager Console).
This command will apply current migration file.

After this project should run fine. Refer to this video in order to understand how most things in .NET Core work: https://www.youtube.com/watch?v=fmvcAzHpsk8&ab_channel=LesJackson.
