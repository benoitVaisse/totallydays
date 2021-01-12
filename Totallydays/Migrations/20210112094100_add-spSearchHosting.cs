using Microsoft.EntityFrameworkCore.Migrations;

namespace Totallydays.Migrations
{
    public partial class addspSearchHosting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			string procedure = @"Create procedure spSearchHosting
									@city varchar(255),
									@date_start datetime,
									@date_end datetime,
									@nbUser int

								as
								Begin
									declare @hosting_ids varchar
									declare @table_ids as table(ids varchar)
									set @hosting_ids =(
										select distinct  STRING_AGG(h.Hosting_id,',') as id from Hostings h
										LEFT JOIN Bookings b ON b.HostingHosting_id = h.Hosting_id
										LEFT JOIN Unavailable_dates ud ON ud.hosting_id = h.Hosting_id
										where  h.City like '%'+@city+'%'
										AND ( 
												(b.Start_date between @date_start and @date_end)
													OR
												(b.End_date  between @date_start and @date_end)
											)
										AND( 
												(ud.Start_date between @date_start and @date_end)
													OR
												(ud.End_date  between @date_start and @date_end)
											)
										group by h.Hosting_id
									)
	
										SELECT DISTINCT h.* ,COUNT(DISTINCT b.Bedroom_id) as rr from Hostings h
										LEFT JOIN Bedrooms b ON b.Hosting_id = h.Hosting_id
										WHERE h.City like '%'+@city+'%'  
										AND h.Hosting_id not in ( IsNull(@hosting_ids, 0))
										AND h.Published = 1 AND h.Active = 1
										GROUP BY h.Hosting_id, h.Active, 
										h.Address, h.City, h.Cover_image, 
										h.CreatedAt, h.Description, h.Hosting_type_id, 
										h.Lat, h.Lng, h.Modified, h.Post_code, h.Price, h.Published, 
										h.Published, h.Resume, h.Slug, h.Title, h.UserId
										HAVING COUNT(b.Bedroom_id) >= @nbUser
	
								End";

			migrationBuilder.Sql(procedure);

		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			string procedure = @"Drop procedure spSearchHosting";
			migrationBuilder.Sql(procedure);

		}
    }
}
