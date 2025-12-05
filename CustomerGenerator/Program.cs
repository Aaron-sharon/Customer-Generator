using System;
using System.IO;
using System.Text;

class Program
{
    static readonly Random random = new Random();

    static readonly string[] firstNames = { "Erik", "Anna", "Lars", "Maria", "Johan", "Karin",
        "Anders", "Sofia", "Magnus", "Emma", "Oskar", "Lisa", "Gustav", "Sara", "Fredrik", "Helena" };

    static readonly string[] lastNames = { "Andersson", "Johansson", "Karlsson", "Nilsson", "Eriksson",
        "Larsson", "Olsson", "Persson", "Svensson", "Gustafsson", "Pettersson", "Jonsson" };

    static readonly string[] streets = { "Strandvägen", "Drottninggatan", "Kungsgatan", "Vasagatan",
        "Sveavägen", "Malmvägen", "Ringvägen", "Birger Jarlsgatan", "Storgatan", "Parkgatan" };

    static readonly string[] cities = { "STOCKHOLM", "GÖTEBORG", "MALMÖ", "UPPSALA", "VÄSTERÅS",
        "ÖREBRO", "LINKÖPING", "HELSINGBORG", "JÖNKÖPING", "NORRKÖPING", "VÄXJÖ", "LUND" };

    static readonly int[] consumptions = { 2000, 3000, 5000, 7000, 10000, 15000, 20000 };

    static void Main(string[] args)
    {
        Console.WriteLine("=== Customer XML Generator ===");
        Console.Write("Enter number of customers to generate (e.g., 20000): ");

        if (!int.TryParse(Console.ReadLine(), out int numCustomers) || numCustomers <= 0)
        {
            Console.WriteLine("Invalid number. Using default: 20000");
            numCustomers = 20000;
        }

        string outputFile = $"customers_{numCustomers}.xml";

        Console.WriteLine($"\nGenerating {numCustomers} customers...");
        Console.WriteLine($"Output file: {outputFile}");

        GenerateCustomersXml(numCustomers, outputFile);

        Console.WriteLine($"\n✓ Complete! File saved: {outputFile}");
        Console.WriteLine($"File size: ~{new FileInfo(outputFile).Length / 1024 / 1024} MB");
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static void GenerateCustomersXml(int numCustomers, string outputFile)
    {
        using (var writer = new StreamWriter(outputFile, false, Encoding.UTF8))
        {
            // Write header
            writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            writer.WriteLine("<customers>");
            WriteStaticHeader(writer);

            // Generate customers
            int batchSize = 1000;
            for (int i = 0; i < numCustomers; i++)
            {
                WriteCustomer(writer, i);

                if ((i + 1) % batchSize == 0)
                {
                    Console.WriteLine($"Progress: {i + 1}/{numCustomers} customers generated...");
                }
            }

            // Write footer
            writer.WriteLine("</customers>");
        }
    }

    static void WriteStaticHeader(StreamWriter writer)
    {
        writer.WriteLine(@"    <zavann_information>
        <organization_system_name>Zavann AB</organization_system_name>
        <phone>+46 10 19 55 099</phone>
        <legal_name/>
        <organization_name>EG Software Sweden AB</organization_name>
        <postal_address>Sankt Göransgatan 63</postal_address>
        <box_address/>
        <zip_code_city>11238 Stockholm</zip_code_city>
        <visiting_address>Sankt Göransgatan 63</visiting_address>
        <visiting_zip_code_city>11238 Stockholm</visiting_zip_code_city>
        <email>support@zavann.se</email>
        <organization_number>556751-5514</organization_number>
        <organization_website>www.zavann.se</organization_website>
        <phone_hours>mån-fre 08-16</phone_hours>
    </zavann_information>
    <general_data>
        <create_date>2025-11-06</create_date>
        <create_time>07:57</create_time>
        <create_month>november</create_month>
        <create_year>2025</create_year>
        <create_day>6</create_day>
        <template_category_id>7100</template_category_id>
    </general_data>
    <standard_templates_settings>
        <logotype>telinet-logo.jpg</logotype>
        <logo_height>40px</logo_height>
        <font>Arial</font>
        <font_size>10pt</font_size>
        <font_color>#000000</font_color>
        <text_box_color/>
        <text_box_border_color/>
        <footer_border_color/>
        <header_color/>
        <logo_position>left</logo_position>
        <header_border_color/>
        <footer_color/>
    </standard_templates_settings>");
    }

    static void WriteCustomer(StreamWriter writer, int index)
    {
        var firstName = firstNames[random.Next(firstNames.Length)];
        var lastName = lastNames[random.Next(lastNames.Length)];
        var street = $"{streets[random.Next(streets.Length)]} {random.Next(1, 201)}{GetRandomSuffix()}";
        var city = cities[random.Next(cities.Length)];
        var zipCode = $"{random.Next(100, 1000)} {random.Next(10, 100)}";

        // Generate birth date (20-70 years old)
        var birthDate = DateTime.Now.AddDays(-random.Next(7300, 25550));
        var socIdFull = $"{birthDate:yyMMdd}-{random.Next(1000, 10000)}";
        var socIdShort = $"{birthDate:yyMMdd}-XXXX";

        // Unique IDs
        int customerId = 6 + index;
        int siteId = 9 + index;
        long objectId = 735999756427205424L + index;

        // Contact info
        var phone1 = GeneratePhone();
        var phone2 = GeneratePhone();
        var phone3 = GeneratePhone();
        var email = $"{firstName.ToLower()}.{lastName.ToLower()}{index}@zavann.net";

        var consumption = consumptions[random.Next(consumptions.Length)];
        var latestReading = consumption * random.Next(2, 6);

        var createDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        writer.WriteLine($@"    <customer>
        <first_page_sequnce_number>0</first_page_sequnce_number>
        <is_password_set>0</is_password_set>
        <phone_numbers>{phone1}, {phone2}, {phone3}</phone_numbers>
        <soc_id_shorten>{socIdShort}</soc_id_shorten>
        <organization_name>Telinet Energi AB</organization_name>
        <organization_legally_name>Telinet Energi AB</organization_legally_name>
        <organization_phone_hours>Mån-tors: 08-17, Fre: 09-17</organization_phone_hours>
        <organization_phone>0771-456 150</organization_phone>
        <master_organization_phone>0771-456 150</master_organization_phone>
        <organization_care_of_address/>
        <organization_address>Oxenstiernsgatan 15 A</organization_address>
        <organization_zip_code_city>115 27 Stockholm</organization_zip_code_city>
        <organization_image>telinet-logo.jpg</organization_image>
        <social_id_for_digital_registration>{socIdFull}</social_id_for_digital_registration>
        <organization_email>kundservice@telinet.se</organization_email>
        <organization_postgiro/>
        <organization_bankgiro>168-6039</organization_bankgiro>
        <organization_bankgiro_ocr>168-6039</organization_bankgiro_ocr>
        <organization_number>556766-5053</organization_number>
        <organization_account_number/>
        <organization_website>www.telinet.se</organization_website>
        <organization_border_city/>
        <organization_box_address/>
        <backgroundColor>#E6E7EC</backgroundColor>
        <headerBackgroundColor>#FFFFFF</headerBackgroundColor>
        <paymentButtonColor>#2dc0d0</paymentButtonColor>
        <linkColor>#0645AD</linkColor>
        <customer_id>{customerId}</customer_id>
        <first_name>{firstName}</first_name>
        <last_name>{lastName}</last_name>
        <company_name/>
        <soc_id_reduced>{socIdShort}</soc_id_reduced>
        <soc_id>{socIdFull}</soc_id>
        <email>{email}</email>
        <organization_sys_name>TELINET</organization_sys_name>
        <organization_id>5</organization_id>
        <care_of/>
        <customer_address>{street}</customer_address>
        <address>{street}</address>
        <street_number/>
        <customer_zip_code>{zipCode}</customer_zip_code>
        <zip_code>{zipCode}</zip_code>
        <customer_city>{city}</customer_city>
        <city>{city}</city>
        <country>Sweden</country>
        <day_phone>{phone1}</day_phone>
        <home_phone>{phone2}</home_phone>
        <cell_phone>{phone3}</cell_phone>
        <create_date>{createDateTime}</create_date>
        <passwd/>
        <signup_date>{createDateTime}</signup_date>
        <contract_source_id>101</contract_source_id>
        <agent_name/>
        <agent_id/>
        <supplier_cancellation_notice/>
        <additional_message/>
        <has_postal_dispatch>0</has_postal_dispatch>
        <internal_id>Ng..</internal_id>
        <customer_type_id>1</customer_type_id>
        <is_company>0</is_company>
        <show_excl_vat>0</show_excl_vat>
        <site>
            <site_id>{siteId}</site_id>
            <campaign_code/>
            <ip_address/>
            <object_id>{objectId}</object_id>
            <serial_id/>
            <consumption>{consumption:N0}</consumption>
            <address>{street}</address>
            <street_number/>
            <zip_code>{zipCode}</zip_code>
            <city>{city}</city>
            <payment_type>Bank giro service</payment_type>
            <electricity_supplier/>
            <cancellation_date/>
            <create_date>{createDateTime}</create_date>
            <delivery_start_date>2024-01-01 00:00:00</delivery_start_date>
            <latest_reading>{latestReading}</latest_reading>
            <latest_reading_date>2025-11-01 00:00:00</latest_reading_date>
            <current_state>Delivery in progress</current_state>
            <current_state_reason/>
            <is_site_complete>1</is_site_complete>
            <consumption_as_int>{consumption}</consumption_as_int>
            <has_received_registration_confirmation>0</has_received_registration_confirmation>
            <net_owner_name>Växjö Energi Elnät AB</net_owner_name>
            <net_owner_name_abbr/>
            <electricity_area_id>4</electricity_area_id>
            <electricity_area_name>SE4</electricity_area_name>
            <invoice_presentation_id>3</invoice_presentation_id>
            <net_area_name>VXO</net_area_name>
            <site_organization_id>5</site_organization_id>
            <site_organization_name>Telinet Energi AB</site_organization_name>
            <is_production_site>0</is_production_site>
            <contract>
                <contract_id>{siteId}</contract_id>
                <periodicity>3</periodicity>
                <transaction_type_id>1</transaction_type_id>
                <termination_type_id>0</termination_type_id>
                <sale_date>2023-11-01</sale_date>
                <start_date>2024-01-01</start_date>
                <con_start_type_id>1</con_start_type_id>
                <create_date>2025-11-06</create_date>
                <ep_product_name>Fast pris</ep_product_name>
                <current_price>102.6</current_price>
                <current_total_price>128.25</current_total_price>
                <monthly_fee>19</monthly_fee>
                <monthly_fee_excluding_vat>15.2</monthly_fee_excluding_vat>
                <fixed_price>1</fixed_price>
                <mixed_price>0</mixed_price>
            </contract>
        </site>
    </customer>");
    }

    static string GeneratePhone()
    {
        return $"0{random.Next(10, 100)}{random.Next(10000000, 100000000)}";
    }

    static string GetRandomSuffix()
    {
        var suffixes = new[] { "A", "B", "C", "" };
        return suffixes[random.Next(suffixes.Length)];
    }
}