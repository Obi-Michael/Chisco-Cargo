using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace DataAccess.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountStatus> AccountStatuses { get; set; } = null!;
        public virtual DbSet<AccountSummary> AccountSummaries { get; set; } = null!;
        public virtual DbSet<AccountTransaction> AccountTransactions { get; set; } = null!;
        public virtual DbSet<Agent> Agents { get; set; } = null!;
        public virtual DbSet<AgentApplicant> AgentApplicants { get; set; } = null!;
        public virtual DbSet<AgentCommission> AgentCommissions { get; set; } = null!;
        public virtual DbSet<AgentLocation> AgentLocations { get; set; } = null!;
        public virtual DbSet<AgentTransaction> AgentTransactions { get; set; } = null!;
        public virtual DbSet<AgentWallet> AgentWallets { get; set; } = null!;
        public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; } = null!;
        public virtual DbSet<ApiClaim> ApiClaims { get; set; } = null!;
        public virtual DbSet<ApiProperty> ApiProperties { get; set; } = null!;
        public virtual DbSet<ApiResource> ApiResources { get; set; } = null!;
        public virtual DbSet<ApiScope> ApiScopes { get; set; } = null!;
        public virtual DbSet<ApiScopeClaim> ApiScopeClaims { get; set; } = null!;
        public virtual DbSet<ApiSecret> ApiSecrets { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Models.Models.Attribute> Attributes { get; set; } = null!;
        //public virtual DbSet<Attribute> Attributes { get; set; } = null!;
        public virtual DbSet<Bank> Banks { get; set; } = null!;
        public virtual DbSet<BankPayment> BankPayments { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BookingType> BookingTypes { get; set; } = null!;
        public virtual DbSet<BrandType> BrandTypes { get; set; } = null!;
        public virtual DbSet<CashRemittant> CashRemittants { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientClaim> ClientClaims { get; set; } = null!;
        public virtual DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; } = null!;
        public virtual DbSet<ClientGrantType> ClientGrantTypes { get; set; } = null!;
        public virtual DbSet<ClientIdPrestriction> ClientIdPrestrictions { get; set; } = null!;
        public virtual DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; } = null!;
        public virtual DbSet<ClientProperty> ClientProperties { get; set; } = null!;
        public virtual DbSet<ClientRedirectUri> ClientRedirectUris { get; set; } = null!;
        public virtual DbSet<ClientScope> ClientScopes { get; set; } = null!;
        public virtual DbSet<ClientSecret> ClientSecrets { get; set; } = null!;
        public virtual DbSet<CompanyInfo> CompanyInfos { get; set; } = null!;
        public virtual DbSet<CompanyType> CompanyTypes { get; set; } = null!;
        public virtual DbSet<Complaint> Complaints { get; set; } = null!;
        public virtual DbSet<Counter> Counters { get; set; } = null!;
        public virtual DbSet<Coupon> Coupons { get; set; } = null!;
        public virtual DbSet<CouponManagement> CouponManagements { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerBio> CustomerBios { get; set; } = null!;
        public virtual DbSet<CustomerCouponRegistration> CustomerCouponRegistrations { get; set; } = null!;
        public virtual DbSet<CustomerInformation> CustomerInformations { get; set; } = null!;
        public virtual DbSet<CustomerPlatformType> CustomerPlatformTypes { get; set; } = null!;
        public virtual DbSet<CustomerType> CustomerTypes { get; set; } = null!;
        public virtual DbSet<CustomerType1> CustomerTypes1 { get; set; } = null!;
        public virtual DbSet<CutomerInvoice> CutomerInvoices { get; set; } = null!;
        public virtual DbSet<DeliveryTypePrice> DeliveryTypePrices { get; set; } = null!;
        public virtual DbSet<Deliverytype> Deliverytypes { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<DeviceCode> DeviceCodes { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<DiscountTransaction> DiscountTransactions { get; set; } = null!;
        public virtual DbSet<DiscountType> DiscountTypes { get; set; } = null!;
        public virtual DbSet<Dispatch> Dispatches { get; set; } = null!;
        public virtual DbSet<Driver> Drivers { get; set; } = null!;
        public virtual DbSet<DriverAccount> DriverAccounts { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeRoute> EmployeeRoutes { get; set; } = null!;
        public virtual DbSet<ErrorCode> ErrorCodes { get; set; } = null!;
        public virtual DbSet<ExcludedSeat> ExcludedSeats { get; set; } = null!;
        public virtual DbSet<Expense> Expenses { get; set; } = null!;
        public virtual DbSet<Fare> Fares { get; set; } = null!;
        public virtual DbSet<FareCalendar> FareCalendars { get; set; } = null!;
        public virtual DbSet<FleetAsset> FleetAssets { get; set; } = null!;
        public virtual DbSet<Franchise> Franchises { get; set; } = null!;
        public virtual DbSet<FranchiseUser> FranchiseUsers { get; set; } = null!;
        public virtual DbSet<Franchize> Franchizes { get; set; } = null!;
        public virtual DbSet<GeneralLedger> GeneralLedgers { get; set; } = null!;
        public virtual DbSet<GeneralTransaction> GeneralTransactions { get; set; } = null!;
        public virtual DbSet<GroupWayBillNumber> GroupWayBillNumbers { get; set; } = null!;
        public virtual DbSet<GroupWaybillNumMapping> GroupWaybillNumMappings { get; set; } = null!;
        public virtual DbSet<GroupshipmentList> GroupshipmentLists { get; set; } = null!;
        public virtual DbSet<Hash> Hashes { get; set; } = null!;
        public virtual DbSet<HireBu> HireBus { get; set; } = null!;
        public virtual DbSet<HirePassenger> HirePassengers { get; set; } = null!;
        public virtual DbSet<HireRequest> HireRequests { get; set; } = null!;
        public virtual DbSet<IdentificationType> IdentificationTypes { get; set; } = null!;
        public virtual DbSet<IdentityClaim> IdentityClaims { get; set; } = null!;
        public virtual DbSet<IdentityProperty> IdentityProperties { get; set; } = null!;
        public virtual DbSet<IdentityResource> IdentityResources { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<InventoryAdjustmentType> InventoryAdjustmentTypes { get; set; } = null!;
        public virtual DbSet<InventoryItem> InventoryItems { get; set; } = null!;
        public virtual DbSet<InventoryReceivedDetail> InventoryReceivedDetails { get; set; } = null!;
        public virtual DbSet<InventoryReceivedHeader> InventoryReceivedHeaders { get; set; } = null!;
        public virtual DbSet<InventoryRequest> InventoryRequests { get; set; } = null!;
        public virtual DbSet<InventoryTracker> InventoryTrackers { get; set; } = null!;
        public virtual DbSet<Issue> Issues { get; set; } = null!;
        public virtual DbSet<ItemCategory> ItemCategories { get; set; } = null!;
        public virtual DbSet<ItemCondition> ItemConditions { get; set; } = null!;
        public virtual DbSet<ItemFamily> ItemFamilies { get; set; } = null!;
        public virtual DbSet<ItemType> ItemTypes { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobParameter> JobParameters { get; set; } = null!;
        public virtual DbSet<JobQueue> JobQueues { get; set; } = null!;
        public virtual DbSet<JourneyManagement> JourneyManagements { get; set; } = null!;
        public virtual DbSet<LedgerChartOfAccount> LedgerChartOfAccounts { get; set; } = null!;
        public virtual DbSet<List> Lists { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<LocationHub> LocationHubs { get; set; } = null!;
        public virtual DbSet<MaintenanceRequest> MaintenanceRequests { get; set; } = null!;
        public virtual DbSet<Manifest> Manifests { get; set; } = null!;
        public virtual DbSet<ManifestMapping> ManifestMappings { get; set; } = null!;
        public virtual DbSet<Mechanic> Mechanics { get; set; } = null!;
        public virtual DbSet<MerchantSignup> MerchantSignups { get; set; } = null!;
        public virtual DbSet<MerchantVolume> MerchantVolumes { get; set; } = null!;
        public virtual DbSet<MerchantWeightRangePrice> MerchantWeightRangePrices { get; set; } = null!;
        public virtual DbSet<MtuPhoto> MtuPhotos { get; set; } = null!;
        public virtual DbSet<MtuReportModel> MtuReportModels { get; set; } = null!;
        public virtual DbSet<NameValueMapping> NameValueMappings { get; set; } = null!;
        public virtual DbSet<NextNumber> NextNumbers { get; set; } = null!;
        public virtual DbSet<OtherIncome> OtherIncomes { get; set; } = null!;
        public virtual DbSet<PackagingPricing> PackagingPricings { get; set; } = null!;
        public virtual DbSet<PassportType> PassportTypes { get; set; } = null!;
        public virtual DbSet<PayStackPaymentResponse> PayStackPaymentResponses { get; set; } = null!;
        public virtual DbSet<PayStackWebhookResponse> PayStackWebhookResponses { get; set; } = null!;
        public virtual DbSet<PaymentGatewayStatus> PaymentGatewayStatuses { get; set; } = null!;
        public virtual DbSet<PersistedGrant> PersistedGrants { get; set; } = null!;
        public virtual DbSet<PickupPoint> PickupPoints { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<PriceCalculator> PriceCalculators { get; set; } = null!;
        public virtual DbSet<ProcurementRequest> ProcurementRequests { get; set; } = null!;
        public virtual DbSet<Quotation> Quotations { get; set; } = null!;
        public virtual DbSet<Referral> Referrals { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        public virtual DbSet<Route> Routes { get; set; } = null!;
        public virtual DbSet<Schema> Schemas { get; set; } = null!;
        public virtual DbSet<SeatManagement> SeatManagements { get; set; } = null!;
        public virtual DbSet<Server> Servers { get; set; } = null!;
        public virtual DbSet<ServiceRequest> ServiceRequests { get; set; } = null!;
        public virtual DbSet<ServiceType> ServiceTypes { get; set; } = null!;
        public virtual DbSet<ServiceTypePrice> ServiceTypePrices { get; set; } = null!;
        public virtual DbSet<Set> Sets { get; set; } = null!;
        public virtual DbSet<Shipment> Shipments { get; set; } = null!;
        public virtual DbSet<ShipmentItem> ShipmentItems { get; set; } = null!;
        public virtual DbSet<ShipmentItemCategory> ShipmentItemCategories { get; set; } = null!;
        public virtual DbSet<ShipmentPackaging> ShipmentPackagings { get; set; } = null!;
        public virtual DbSet<ShipmentParcel> ShipmentParcels { get; set; } = null!;
        public virtual DbSet<ShipmentRequest> ShipmentRequests { get; set; } = null!;
        public virtual DbSet<ShipmentCollection> ShipmentCollections { get; set; } = null!;
        public virtual DbSet<ShipmentTracking> ShipmentTrackings { get; set; } = null!;
        public virtual DbSet<Smsprofile> Smsprofiles { get; set; } = null!;
        public virtual DbSet<SpecialPackage> SpecialPackages { get; set; } = null!;
        public virtual DbSet<SpecialPackagepricing> SpecialPackagepricings { get; set; } = null!;
        public virtual DbSet<SpecialShipmentPrice> SpecialShipmentPrices { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<SubRoute> SubRoutes { get; set; } = null!;
        public virtual DbSet<Terminal> Terminals { get; set; } = null!;
        public virtual DbSet<Trip> Trips { get; set; } = null!;
        public virtual DbSet<TripAvailability> TripAvailabilities { get; set; } = null!;
        public virtual DbSet<TripSetting> TripSettings { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<VehicleAllocationDetail> VehicleAllocationDetails { get; set; } = null!;
        public virtual DbSet<VehicleMake> VehicleMakes { get; set; } = null!;
        public virtual DbSet<VehicleMileage> VehicleMileages { get; set; } = null!;
        public virtual DbSet<VehicleModel> VehicleModels { get; set; } = null!;
        public virtual DbSet<VehiclePart> VehicleParts { get; set; } = null!;
        public virtual DbSet<VehiclePartPosition> VehiclePartPositions { get; set; } = null!;
        public virtual DbSet<VehiclePartRegistration> VehiclePartRegistrations { get; set; } = null!;
        public virtual DbSet<VehicleTripRegistration> VehicleTripRegistrations { get; set; } = null!;
        public virtual DbSet<Vehiclemodel1> Vehiclemodels1 { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;
        public virtual DbSet<VendorInformation> VendorInformations { get; set; } = null!;
        public virtual DbSet<VendorType> VendorTypes { get; set; } = null!;
        public virtual DbSet<VolumeRange> VolumeRanges { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;
        public virtual DbSet<WalletNumber> WalletNumbers { get; set; } = null!;
        public virtual DbSet<WalletTransaction> WalletTransactions { get; set; } = null!;
        public virtual DbSet<Warehouse> Warehouses { get; set; } = null!;
        public virtual DbSet<WarehouseBin> WarehouseBins { get; set; } = null!;
        public virtual DbSet<WarehouseBinType> WarehouseBinTypes { get; set; } = null!;
        public virtual DbSet<WayBill> WayBills { get; set; } = null!;
        public virtual DbSet<WeightRange> WeightRanges { get; set; } = null!;
        public virtual DbSet<Workshop> Workshops { get; set; } = null!;
        public virtual DbSet<Zone> Zones { get; set; } = null!;
        public virtual DbSet<ZoneMapping> ZoneMappings { get; set; } = null!;
        public virtual DbSet<ZonePrice> ZonePrices { get; set; } = null!;
        public virtual DbSet<ZonePricePerKg> ZonePricePerKgs { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Chisco_Latest_Db;Trusted_Connection=True;TrustServerCertificate=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");

            modelBuilder.Entity<AccountStatus>(entity =>
            {
                entity.Property(e => e.AccountStatus1).HasColumnName("AccountStatus");
            });

            modelBuilder.Entity<AccountSummary>(entity =>
            {
                entity.ToTable("AccountSummary");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AccountTransaction>(entity =>
            {
                entity.ToTable("AccountTransaction");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("Agent");

                entity.Property(e => e.WalletBalance).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<AgentApplicant>(entity =>
            {
                entity.ToTable("AgentApplicant");
            });

            modelBuilder.Entity<AgentCommission>(entity =>
            {
                entity.ToTable("AgentCommission");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<AgentLocation>(entity =>
            {
                entity.ToTable("AgentLocation");
            });

            modelBuilder.Entity<AgentTransaction>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsApproved).HasColumnName("isApproved");

                entity.Property(e => e.IsVerified).HasColumnName("isVerified");
            });

            modelBuilder.Entity<AgentWallet>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_HangFire_CounterAggregated");

                entity.ToTable("AggregatedCounter", "HangFire");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<ApiClaim>(entity =>
            {
                entity.Property(e => e.Type).HasMaxLength(200);
            });

            modelBuilder.Entity<ApiProperty>(entity =>
            {
                entity.Property(e => e.Key).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(2000);
            });

            modelBuilder.Entity<ApiResource>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DisplayName).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<ApiScope>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DisplayName).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<ApiScopeClaim>(entity =>
            {
                entity.Property(e => e.Type).HasMaxLength(200);
            });

            modelBuilder.Entity<ApiSecret>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Type).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(4000);
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Otp).HasColumnName("OTP");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("Bank");
            });

            modelBuilder.Entity<BankPayment>(entity =>
            {
                entity.ToTable("BankPayment");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.PayStackReference).HasMaxLength(450);

                entity.Property(e => e.PayStackWebhookReference).HasMaxLength(450);
            });

            modelBuilder.Entity<BookingType>(entity =>
            {
                entity.ToTable("BookingType");
            });

            modelBuilder.Entity<BrandType>(entity =>
            {
                entity.ToTable("BrandType");
            });

            modelBuilder.Entity<CashRemittant>(entity =>
            {
                entity.ToTable("CashRemittant");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.BackChannelLogoutUri).HasMaxLength(2000);

                entity.Property(e => e.ClientClaimsPrefix).HasMaxLength(200);

                entity.Property(e => e.ClientId).HasMaxLength(200);

                entity.Property(e => e.ClientName).HasMaxLength(200);

                entity.Property(e => e.ClientUri).HasMaxLength(2000);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FrontChannelLogoutUri).HasMaxLength(2000);

                entity.Property(e => e.LogoUri).HasMaxLength(2000);

                entity.Property(e => e.PairWiseSubjectSalt).HasMaxLength(200);

                entity.Property(e => e.ProtocolType).HasMaxLength(200);

                entity.Property(e => e.UserCodeType).HasMaxLength(100);
            });

            modelBuilder.Entity<ClientClaim>(entity =>
            {
                entity.Property(e => e.Type).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(250);
            });

            modelBuilder.Entity<ClientCorsOrigin>(entity =>
            {
                entity.Property(e => e.Origin).HasMaxLength(150);
            });

            modelBuilder.Entity<ClientGrantType>(entity =>
            {
                entity.Property(e => e.GrantType).HasMaxLength(250);
            });

            modelBuilder.Entity<ClientIdPrestriction>(entity =>
            {
                entity.ToTable("ClientIdPRestrictions");

                entity.Property(e => e.Provider).HasMaxLength(200);
            });

            modelBuilder.Entity<ClientPostLogoutRedirectUri>(entity =>
            {
                entity.Property(e => e.PostLogoutRedirectUri).HasMaxLength(2000);
            });

            modelBuilder.Entity<ClientProperty>(entity =>
            {
                entity.Property(e => e.Key).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(2000);
            });

            modelBuilder.Entity<ClientRedirectUri>(entity =>
            {
                entity.Property(e => e.RedirectUri).HasMaxLength(2000);
            });

            modelBuilder.Entity<ClientScope>(entity =>
            {
                entity.Property(e => e.Scope).HasMaxLength(200);
            });

            modelBuilder.Entity<ClientSecret>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Type).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(4000);
            });

            modelBuilder.Entity<CompanyInfo>(entity =>
            {
                entity.ToTable("CompanyInfo");

                entity.Property(e => e.TransportVat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("transportVat");
            });

            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.ToTable("Complaint");

                entity.Property(e => e.CreationTime).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.TransDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Counter", "HangFire");

                entity.HasIndex(e => e.Key, "CX_HangFire_Counter")
                    .IsClustered();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key).HasMaxLength(100);
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.ToTable("Coupon");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CouponValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CouponValueLimit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Reference).HasColumnName("reference");
            });

            modelBuilder.Entity<CouponManagement>(entity =>
            {
                entity.ToTable("CouponManagement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CouponCode).IsUnicode(false);

                entity.Property(e => e.CouponUserId).HasMaxLength(100);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.UsedDate).HasColumnType("datetime");

                entity.Property(e => e.Waybill).HasMaxLength(100);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("customer_PK")
                    .IsClustered(false);

                entity.ToTable("customer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DateofBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(500)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.LastName)
                    .HasMaxLength(500)
                    .HasColumnName("lastName");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.UserId).HasMaxLength(128);
            });

            modelBuilder.Entity<CustomerBio>(entity =>
            {
                entity.ToTable("CustomerBio");

                entity.Property(e => e.WalletBalance).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<CustomerCouponRegistration>(entity =>
            {
                entity.ToTable("CustomerCouponRegistration");
            });

            modelBuilder.Entity<CustomerInformation>(entity =>
            {
                entity.ToTable("CustomerInformation");

                entity.Property(e => e.AccountBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreditLimit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.CustomerTypeId).HasColumnName("CustomerTypeID");

                entity.Property(e => e.GlsalesAccount).HasColumnName("GLSalesAccount");

                entity.Property(e => e.ReferalUrl).HasColumnName("ReferalURL");

                entity.Property(e => e.ShipMethodId).HasColumnName("ShipMethodID");

                entity.Property(e => e.TaxGroupId).HasColumnName("TaxGroupID");

                entity.Property(e => e.TaxIdno).HasColumnName("TaxIDNo");

                entity.Property(e => e.TermsId).HasColumnName("TermsID");

                entity.Property(e => e.VattaxIdnumber).HasColumnName("VATTaxIDNumber");

                entity.Property(e => e.WarehouseGlaccount).HasColumnName("WarehouseGLAccount");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");
            });

            modelBuilder.Entity<CustomerPlatformType>(entity =>
            {
                entity.ToTable("CustomerPlatformType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatorUserId).HasMaxLength(255);

                entity.Property(e => e.LastModificationDate).HasColumnType("datetime");

                entity.Property(e => e.LastModificationUserId).HasMaxLength(255);

                entity.Property(e => e.PlatformType).HasMaxLength(255);
            });

            modelBuilder.Entity<CustomerType>(entity =>
            {
                entity.ToTable("CustomerType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CusType)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            });

            modelBuilder.Entity<CustomerType1>(entity =>
            {
                entity.ToTable("CustomerTypes");

                entity.Property(e => e.CoscontrolAccount).HasColumnName("COSControlAccount");
            });

            modelBuilder.Entity<CutomerInvoice>(entity =>
            {
                entity.HasKey(e => e.Int)
                    .HasName("cutomerInvoice_PK")
                    .IsClustered(false);

                entity.ToTable("cutomerInvoice");

                entity.Property(e => e.Int).HasColumnName("int");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateDue)
                    .HasColumnType("datetime")
                    .HasColumnName("dateDue");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasColumnName("dateModified")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DatePaid)
                    .HasColumnType("datetime")
                    .HasColumnName("datePaid");

                entity.Property(e => e.InvoiceNo)
                    .HasMaxLength(500)
                    .HasColumnName("invoiceNo");

                entity.Property(e => e.LocationId).HasColumnName("locationId");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(500)
                    .HasColumnName("paymentMethod");

                entity.Property(e => e.PaymentRef).HasColumnName("paymentRef");

                entity.Property(e => e.PaymentStatus).HasColumnName("paymentStatus");

                entity.Property(e => e.ShipmentCollected).HasColumnName("shipmentCollected");

                entity.Property(e => e.WaybillNumber).HasColumnName("waybillNumber");
            });

            modelBuilder.Entity<DeliveryTypePrice>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("DeliveryTypePrice_PK")
                    .IsClustered(false);

                entity.ToTable("DeliveryTypePrice");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DeliveryTypeId).HasColumnName("deliveryTypeId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.ZoneId).HasColumnName("zoneId");

                entity.HasOne(d => d.DeliveryType)
                    .WithMany(p => p.DeliveryTypePrices)
                    .HasForeignKey(d => d.DeliveryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DeliveryTypePrice_DeliveryTypePrice");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.DeliveryTypePrices)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DeliveryTypePrice_Zone_FK");
            });

            modelBuilder.Entity<Deliverytype>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Deliverytype_PK")
                    .IsClustered(false);

                entity.ToTable("Deliverytype");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.CustomerTypeNavigation)
                    .WithMany(p => p.Deliverytypes)
                    .HasForeignKey(d => d.CustomerType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerType_Deliverytype_FK");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");
            });

            modelBuilder.Entity<DeviceCode>(entity =>
            {
                entity.HasKey(e => e.UserCode);

                entity.Property(e => e.UserCode).HasMaxLength(200);

                entity.Property(e => e.ClientId).HasMaxLength(200);

                entity.Property(e => e.DeviceCode1)
                    .HasMaxLength(200)
                    .HasColumnName("DeviceCode");

                entity.Property(e => e.SubjectId).HasMaxLength(200);
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("Discount");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdultDiscount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AppDiscountAndroid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AppDiscountIos).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AppDiscountWeb).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AppReturnDiscountAndroid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AppReturnDiscountIos).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AppReturnDiscountWeb).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CustomerDiscount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MemberDiscount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinorDiscount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PromoDiscount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReturnDiscount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<DiscountTransaction>(entity =>
            {
                entity.ToTable("DiscountTransaction");
            });

            modelBuilder.Entity<DiscountType>(entity =>
            {
                entity.ToTable("DiscountType");
            });

            modelBuilder.Entity<Dispatch>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("dispatch_PK")
                    .IsClustered(false);

                entity.ToTable("dispatch");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DepartureId).HasColumnName("departureId");

                entity.Property(e => e.Detinationid).HasColumnName("detinationid");

                entity.Property(e => e.DispatchFee)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("dispatchFee");

                entity.Property(e => e.Dispatchedby).HasColumnName("dispatchedby");

                entity.Property(e => e.DriverInfo).HasColumnName("driverInfo");

                entity.Property(e => e.ManifestNumber).HasMaxLength(50);

                entity.Property(e => e.ReceivedBy).HasColumnName("receivedBy");

                entity.Property(e => e.VehicleRegnum).HasColumnName("vehicleRegnum");

                entity.HasOne(d => d.Departure)
                    .WithMany(p => p.Dispatches)
                    .HasForeignKey(d => d.DepartureId)
                    .HasConstraintName("dispatch_Location_FK");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("Driver");
            });

            modelBuilder.Entity<DriverAccount>(entity =>
            {
                entity.ToTable("DriverAccount");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeCode).HasMaxLength(150);

                entity.Property(e => e.OtplastUsedDate).HasColumnName("OTPLastUsedDate");

                entity.Property(e => e.WalletBalance).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_AspNetUsers");
            });

            modelBuilder.Entity<EmployeeRoute>(entity =>
            {
                entity.ToTable("EmployeeRoute");
            });

            modelBuilder.Entity<ErrorCode>(entity =>
            {
                entity.ToTable("ErrorCode");
            });

            modelBuilder.Entity<ExcludedSeat>(entity =>
            {
                entity.ToTable("ExcludedSeat");
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable("Expense");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Fare>(entity =>
            {
                entity.ToTable("Fare");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NonIdAmount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<FareCalendar>(entity =>
            {
                entity.ToTable("FareCalendar");

                entity.Property(e => e.FareValue).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<FleetAsset>(entity =>
            {
                entity.ToTable("FleetAsset");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Franchise>(entity =>
            {
                entity.ToTable("Franchise");
            });

            modelBuilder.Entity<FranchiseUser>(entity =>
            {
                entity.ToTable("FranchiseUser");

                entity.Property(e => e.Otp).HasColumnName("OTP");
            });

            modelBuilder.Entity<Franchize>(entity =>
            {
                entity.ToTable("Franchize");

                entity.Property(e => e.Otp).HasColumnName("OTP");
            });

            modelBuilder.Entity<GeneralLedger>(entity =>
            {
                entity.ToTable("GeneralLedger");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<GeneralTransaction>(entity =>
            {
                entity.ToTable("GeneralTransaction");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TransactionAmount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<GroupWayBillNumber>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("GroupWayBillNumber");

                entity.Property(e => e.ArrivalId).HasColumnName("arrivalId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DepartureId).HasColumnName("departureId");

                entity.Property(e => e.HasManifest).HasColumnName("hasManifest");

                entity.Property(e => e.SentToHub).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<GroupWaybillNumMapping>(entity =>
            {
                entity.HasKey(e => e.MappingId)
                    .HasName("PK_dbo.GroupWaybillNumMapping");

                entity.ToTable("GroupWaybillNumMapping");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.GroupWaybillNumber).HasMaxLength(100);

                entity.Property(e => e.WaybillNumber).HasMaxLength(100);

                entity.HasOne(d => d.Departure)
                    .WithMany(p => p.GroupWaybillNumMappingDepartures)
                    .HasForeignKey(d => d.DepartureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupWaybillNumMapping_Location");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.GroupWaybillNumMappingDestinations)
                    .HasForeignKey(d => d.DestinationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupWaybillNumMapping_Location1");
            });

            modelBuilder.Entity<GroupshipmentList>(entity =>
            {
                entity.ToTable("GroupshipmentList");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Groupnumber).HasColumnName("groupnumber");
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_HangFire_Hash");

                entity.ToTable("Hash", "HangFire");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<HireBu>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<HirePassenger>(entity =>
            {
                entity.ToTable("HirePassenger");
            });

            modelBuilder.Entity<HireRequest>(entity =>
            {
                entity.ToTable("HireRequest");
            });

            modelBuilder.Entity<IdentificationType>(entity =>
            {
                entity.ToTable("IdentificationType");
            });

            modelBuilder.Entity<IdentityClaim>(entity =>
            {
                entity.Property(e => e.Type).HasMaxLength(200);
            });

            modelBuilder.Entity<IdentityProperty>(entity =>
            {
                entity.Property(e => e.Key).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(2000);
            });

            modelBuilder.Entity<IdentityResource>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.DisplayName).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.WarehouseBinId).HasColumnName("WarehouseBinID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");
            });

            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.Property(e => e.Average).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AverageCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AverageValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BrandTypeId).HasColumnName("BrandTypeID");

                entity.Property(e => e.Expected).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExpectedCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExpectedValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Fifocost)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("FIFOCost");

                entity.Property(e => e.Fifofifovalue)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("FIFOFIFOValue");

                entity.Property(e => e.GlitemCogsaccountId).HasColumnName("GLItemCOGSAccountId");

                entity.Property(e => e.GlitemCogsaccountName).HasColumnName("GLItemCOGSAccountName");

                entity.Property(e => e.GlitemInventoryAccountId).HasColumnName("GLItemInventoryAccountId");

                entity.Property(e => e.GlitemInventoryAccountName).HasColumnName("GLItemInventoryAccountName");

                entity.Property(e => e.GlitemSalesAccountId).HasColumnName("GLItemSalesAccountId");

                entity.Property(e => e.GlitemSalesAccountName).HasColumnName("GLItemSalesAccountName");

                entity.Property(e => e.ItemCategoryId).HasColumnName("ItemCategoryID");

                entity.Property(e => e.ItemDefaultWarehouseBinId).HasColumnName("ItemDefaultWarehouseBinID");

                entity.Property(e => e.ItemDefaultWarehouseId).HasColumnName("ItemDefaultWarehouseID");

                entity.Property(e => e.ItemDescription).HasColumnName("itemDescription");

                entity.Property(e => e.ItemFamilyId).HasColumnName("ItemFamilyID");

                entity.Property(e => e.ItemUom).HasColumnName("ItemUOM");

                entity.Property(e => e.ItemUpccode).HasColumnName("ItemUPCCode");

                entity.Property(e => e.Lifo)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("LIFO");

                entity.Property(e => e.Lifocost)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("LIFOCost");

                entity.Property(e => e.Lifovalue)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("LIFOValue");

                entity.Property(e => e.PictureUrl).HasColumnName("PictureURL");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");
            });

            modelBuilder.Entity<InventoryReceivedDetail>(entity =>
            {
                entity.ToTable("InventoryReceivedDetail");

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.Property(e => e.GlanalysisType1).HasColumnName("GLAnalysisType1");

                entity.Property(e => e.GlanalysisType2).HasColumnName("GLAnalysisType2");

                entity.Property(e => e.GlexpenseAccount).HasColumnName("GLExpenseAccount");

                entity.Property(e => e.InventoryReceivedId).HasColumnName("InventoryReceivedID");

                entity.Property(e => e.InventoryTransferId).HasColumnName("InventoryTransferID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemUpccode).HasColumnName("ItemUPCCode");

                entity.Property(e => e.Ponumber).HasColumnName("PONumber");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.ToWarehouseBinId).HasColumnName("ToWarehouseBinID");

                entity.Property(e => e.ToWarehouseId).HasColumnName("ToWarehouseID");

                entity.Property(e => e.WarehouseBinId).HasColumnName("WarehouseBinID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");
            });

            modelBuilder.Entity<InventoryReceivedHeader>(entity =>
            {
                entity.ToTable("InventoryReceivedHeader");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdjustmentTypeId).HasColumnName("AdjustmentTypeID");

                entity.Property(e => e.CurrencyExchangeRate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FromCompanyId).HasColumnName("FromCompanyID");

                entity.Property(e => e.FromWarehouseBinId).HasColumnName("FromWarehouseBinID");

                entity.Property(e => e.FromWarehouseId).HasColumnName("FromWarehouseID");

                entity.Property(e => e.InventoryIssueTransferId).HasColumnName("InventoryIssueTransferID");

                entity.Property(e => e.WarehouseBinId).HasColumnName("WarehouseBinID");

                entity.Property(e => e.WarehouseCustomerId).HasColumnName("WarehouseCustomerID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");
            });

            modelBuilder.Entity<InventoryRequest>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CompanyTypeId).HasColumnName("CompanyTypeID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.IsApproved).HasColumnName("isApproved");

                entity.Property(e => e.IsCaptured).HasColumnName("isCaptured");

                entity.Property(e => e.IsIssued).HasColumnName("isIssued");

                entity.Property(e => e.IsVerified).HasColumnName("isVerified");

                entity.Property(e => e.ItemFourId).HasColumnName("ItemFourID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemThreeId).HasColumnName("ItemThreeID");

                entity.Property(e => e.ItemTwoId).HasColumnName("ItemTwoID");

                entity.Property(e => e.MechanicId).HasColumnName("MechanicID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VehicleRegId).HasColumnName("VehicleRegID");

                entity.Property(e => e.WarehouseBinId).HasColumnName("WarehouseBinID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");
            });

            modelBuilder.Entity<InventoryTracker>(entity =>
            {
                entity.ToTable("InventoryTracker");

                entity.Property(e => e.NewPrice).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.ToTable("Issue");
            });

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                entity.Property(e => e.CategoryPictureUrl).HasColumnName("CategoryPictureURL");

                entity.Property(e => e.ItemFamilyId).HasColumnName("ItemFamilyID");
            });

            modelBuilder.Entity<ItemCondition>(entity =>
            {
                entity.ToTable("ItemCondition");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ItemCondition1)
                    .IsUnicode(false)
                    .HasColumnName("ItemCondition");
            });

            modelBuilder.Entity<ItemFamily>(entity =>
            {
                entity.Property(e => e.FamilyPictureUrl).HasColumnName("FamilyPictureURL");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_HangFire_JobParameter");

                entity.ToTable("JobParameter", "HangFire");

                entity.Property(e => e.Name).HasMaxLength(40);
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.HasKey(e => new { e.Queue, e.Id })
                    .HasName("PK_HangFire_JobQueue");

                entity.ToTable("JobQueue", "HangFire");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<JourneyManagement>(entity =>
            {
                entity.ToTable("JourneyManagement");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CaptainFee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DispatchFee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LoaderFee).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<LedgerChartOfAccount>(entity =>
            {
                entity.Property(e => e.CglaccountNumber).HasColumnName("CGLAccountNumber");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.GlaccountBalance)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("GLAccountBalance");

                entity.Property(e => e.GlaccountBeginningBalance)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("GLAccountBeginningBalance");

                entity.Property(e => e.GlaccountDescription).HasColumnName("GLAccountDescription");

                entity.Property(e => e.GlaccountName).HasColumnName("GLAccountName");

                entity.Property(e => e.GlaccountType).HasColumnName("GLAccountType");

                entity.Property(e => e.GlaccountUse).HasColumnName("GLAccountUse");

                entity.Property(e => e.GlbalanceType).HasColumnName("GLBalanceType");

                entity.Property(e => e.GlotherNotes).HasColumnName("GLOtherNotes");

                entity.Property(e => e.GlreportingAccount).HasColumnName("GLReportingAccount");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_List");

                entity.ToTable("List", "HangFire");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Location_PK")
                    .IsClustered(false);

                entity.ToTable("Location");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("locationName");

                entity.Property(e => e.StateId).HasColumnName("stateId");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Location_State_FK");
            });

            modelBuilder.Entity<LocationHub>(entity =>
            {
                entity.ToTable("LocationHub");

                entity.HasOne(d => d.Hub)
                    .WithMany(p => p.LocationHubHubs)
                    .HasForeignKey(d => d.HubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationHub_Location");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.LocationHubLocations)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationHub_Hub");
            });

            modelBuilder.Entity<MaintenanceRequest>(entity =>
            {
                entity.ToTable("MaintenanceRequest");

                entity.Property(e => e.CompanyTypeId).HasColumnName("CompanyTypeID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.IsApproved).HasColumnName("isApproved");

                entity.Property(e => e.IsCaptured).HasColumnName("isCaptured");

                entity.Property(e => e.IsIssued).HasColumnName("isIssued");

                entity.Property(e => e.IsVerified).HasColumnName("isVerified");

                entity.Property(e => e.MechanicId).HasColumnName("MechanicID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VehicleRegId).HasColumnName("VehicleRegID");
            });

            modelBuilder.Entity<Manifest>(entity =>
            {
                entity.ToTable("Manifest");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BorderExpense).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Commision).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ConductorAllowance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Dispatch).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DriverAllowance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DriverFee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Feeding).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GasAllowance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LoaderCommission).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Maintenance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mtu)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("MTU");

                entity.Property(e => e.PettyCash).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SpareDriverAllowance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Transit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Transload).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Union).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT");
            });

            modelBuilder.Entity<ManifestMapping>(entity =>
            {
                entity.ToTable("ManifestMapping");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MerchantSignup>(entity =>
            {
                entity.ToTable("MerchantSignup");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Businessname).HasColumnName("businessname");

                entity.Property(e => e.Businesstype).HasColumnName("businesstype");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.Datecreated)
                    .HasColumnType("datetime")
                    .HasColumnName("datecreated");

                entity.Property(e => e.Emailladdress).HasColumnName("emailladdress");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.FixedPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FixedPriceInterState).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .HasColumnName("lastname");

                entity.Property(e => e.MaximumDropOffCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaximumPickUpCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinimumDropOffCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinimumPickUpCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Offpeak).HasColumnName("offpeak");

                entity.Property(e => e.Peakperiod).HasColumnName("peakperiod");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.PricePerKm).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .HasColumnName("state");

                entity.Property(e => e.UserId).HasMaxLength(128);
            });

            modelBuilder.Entity<MerchantVolume>(entity =>
            {
                entity.ToTable("MerchantVolume");
            });

            modelBuilder.Entity<MerchantWeightRangePrice>(entity =>
            {
                entity.ToTable("MerchantWeightRangePrice");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.WeightPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WeightPriceInterState)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.MerchantWeightRangePrices)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MerchantW__Merch__7F80E8EA");

                entity.HasOne(d => d.WeightRange)
                    .WithMany(p => p.MerchantWeightRangePrices)
                    .HasForeignKey(d => d.WeightRangeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MerchantW__Weigh__00750D23");
            });

            modelBuilder.Entity<MtuPhoto>(entity =>
            {
                entity.ToTable("MtuPhoto");
            });

            modelBuilder.Entity<MtuReportModel>(entity =>
            {
                entity.ToTable("MtuReportModel");
            });

            modelBuilder.Entity<NameValueMapping>(entity =>
            {
                entity.ToTable("NameValueMapping");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Value).IsUnicode(false);
            });

            modelBuilder.Entity<OtherIncome>(entity =>
            {
                entity.ToTable("OtherIncome");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<PackagingPricing>(entity =>
            {
                entity.ToTable("PackagingPricing");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("dateCreated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasColumnName("dateModified");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price")
                    .HasDefaultValueSql("((0.00))");
            });

            modelBuilder.Entity<PassportType>(entity =>
            {
                entity.ToTable("PassportType");

                entity.Property(e => e.AddOnFare).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<PayStackPaymentResponse>(entity =>
            {
                entity.HasKey(e => e.Reference);

                entity.ToTable("PayStackPaymentResponse");
            });

            modelBuilder.Entity<PayStackWebhookResponse>(entity =>
            {
                entity.HasKey(e => e.Reference);

                entity.ToTable("PayStackWebhookResponse");
            });

            modelBuilder.Entity<PaymentGatewayStatus>(entity =>
            {
                entity.ToTable("PaymentGatewayStatus");
            });

            modelBuilder.Entity<PersistedGrant>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key).HasMaxLength(200);

                entity.Property(e => e.ClientId).HasMaxLength(200);

                entity.Property(e => e.SubjectId).HasMaxLength(200);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<PickupPoint>(entity =>
            {
                entity.ToTable("PickupPoint");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");
            });

            modelBuilder.Entity<PriceCalculator>(entity =>
            {
                entity.ToTable("PriceCalculator");

                entity.Property(e => e.CommissionforBike).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CommissionforTruck).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CommissionforVan).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DefaultBikePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DefaultTruckPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DefaultVanPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DropOffPriceforBike).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DropOffPriceforTruck).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DropOffPriceforVan).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaximumInKm).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaximumInValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaximumPickUpCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaximumPriceTrigger).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinimumInKm).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinimumInValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinimumPickUpCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinimumPriceTrigger).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PriceTdrforBike)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PriceTDRforBike");

                entity.Property(e => e.PriceTdrforTruck)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PriceTDRforTruck");

                entity.Property(e => e.PriceTdrforVan)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PriceTDRforVan");

                entity.Property(e => e.TerminalPriceForTruck).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ProcurementRequest>(entity =>
            {
                entity.Property(e => e.CompanyTypeId).HasColumnName("CompanyTypeID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.IsApproved).HasColumnName("isApproved");

                entity.Property(e => e.IsCaptured).HasColumnName("isCaptured");

                entity.Property(e => e.IsIssued).HasColumnName("isIssued");

                entity.Property(e => e.IsVerified).HasColumnName("isVerified");

                entity.Property(e => e.ItemFourId).HasColumnName("ItemFourID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemThreeId).HasColumnName("ItemThreeID");

                entity.Property(e => e.ItemTwoId).HasColumnName("ItemTwoID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WarehouseBinId).HasColumnName("WarehouseBinID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");
            });

            modelBuilder.Entity<Quotation>(entity =>
            {
                entity.ToTable("Quotation");

                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DestinationAddress).IsUnicode(false);

                entity.Property(e => e.EmailAddress).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.PackageDescription).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.Property(e => e.PickUpDateTime).HasColumnType("datetime");

                entity.Property(e => e.PickupAddress).IsUnicode(false);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("Route");

                entity.Property(e => e.DispatchFee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DriverFee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LoaderFee).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<SeatManagement>(entity =>
            {
                entity.ToTable("SeatManagement");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PartCash).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Posreference).HasColumnName("POSReference");

                entity.Property(e => e.RerouteFeeDiff).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpgradeDowngradeDiff).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vat).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.Property(e => e.Id).HasMaxLength(100);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<ServiceRequest>(entity =>
            {
                entity.ToTable("ServiceRequest");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.IsApproved).HasColumnName("isApproved");

                entity.Property(e => e.IsCaptured).HasColumnName("isCaptured");

                entity.Property(e => e.IsIssued).HasColumnName("isIssued");

                entity.Property(e => e.IsVerified).HasColumnName("isVerified");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT");
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.ToTable("ServiceType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatorUserId).HasMaxLength(255);

                entity.Property(e => e.LastModificationDate).HasColumnType("datetime");

                entity.Property(e => e.LastModificationUserId).HasMaxLength(255);

                entity.Property(e => e.ServiceType1)
                    .HasMaxLength(255)
                    .HasColumnName("ServiceType");
            });

            modelBuilder.Entity<ServiceTypePrice>(entity =>
            {
                entity.ToTable("ServiceTypePrice");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value })
                    .HasName("PK_HangFire_Set");

                entity.ToTable("Set", "HangFire");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("shipment_PK")
                    .IsClustered(false);

                entity.ToTable("shipment");

                entity.Property(e => e.ActualAmountCollected)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("actualAmountCollected");

                entity.Property(e => e.ActualArrivalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("actualArrivalDate");

                entity.Property(e => e.CashOnDeliveryAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("cashOnDeliveryAmount");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreditPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DeClearedValue)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("deClearedValue");

                entity.Property(e => e.DeliveryTime).HasColumnType("datetime");

                entity.Property(e => e.DeliveryTypeId).HasColumnName("deliveryTypeId");

                entity.Property(e => e.DepartureLocationId).HasColumnName("departureLocationId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DestinationId).HasColumnName("destinationId");

                entity.Property(e => e.DiscountAmountGiven)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("discountAmountGiven");

                entity.Property(e => e.ExpectedAmountToCollect)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("expectedAmountToCollect");

                entity.Property(e => e.ExpectedDateOfArrival)
                    .HasColumnType("datetime")
                    .HasColumnName("expectedDateOfArrival");

                entity.Property(e => e.GrandTotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("grandTotal");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(100)
                    .HasColumnName("groupId");

                entity.Property(e => e.InsuranceAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("insuranceAmount");

                entity.Property(e => e.IsCancelled).HasColumnName("isCancelled");

                entity.Property(e => e.IsCashOnDelivery).HasColumnName("isCashOnDelivery");

                entity.Property(e => e.IsCollected).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.IsInsured).HasColumnName("isInsured");

                entity.Property(e => e.IsMissingDate).HasColumnType("datetime");

                entity.Property(e => e.ItemQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ItemsWeight)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("itemsWeight");

                entity.Property(e => e.MerchantShipmentId).HasColumnName("MerchantShipmentID");

                entity.Property(e => e.Packagingfee)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("packagingfee");

                entity.Property(e => e.PayStackReference).HasMaxLength(450);

                entity.Property(e => e.PayStackWebhookReference).HasMaxLength(450);

                entity.Property(e => e.PaymentMethod).HasColumnName("paymentMethod");

                entity.Property(e => e.PosreferenceNo)
                    .HasMaxLength(255)
                    .HasColumnName("POSReferenceNO");

                entity.Property(e => e.ReceiverAddress).HasColumnName("receiverAddress");

                entity.Property(e => e.ReceiverEmail).HasColumnName("receiverEmail");

                entity.Property(e => e.ReceiverName).HasColumnName("receiverName");

                entity.Property(e => e.ReceiverPhoneNumber).HasColumnName("receiverPhoneNumber");

                entity.Property(e => e.ReceiverStateId).HasColumnName("receiverStateId");

                entity.Property(e => e.RefundAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SenderAddress).HasColumnName("senderAddress");

                entity.Property(e => e.SenderStateId).HasColumnName("senderStateId");

                entity.Property(e => e.Specialnote).HasColumnName("specialnote");

                entity.Property(e => e.TotalTopay)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("totalTopay");

                entity.Property(e => e.TransferDate).HasMaxLength(255);

                entity.Property(e => e.TransferName).HasMaxLength(255);

                entity.Property(e => e.TypeofCustomer).HasColumnName("typeofCustomer");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.ValueIsDecleared).HasColumnName("valueIsDecleared");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("vat");

                entity.Property(e => e.VerifiedAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Waybill).HasMaxLength(100);

                entity.HasOne(d => d.DeliveryType)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.DeliveryTypeId)
                    .HasConstraintName("shipment_Deliverytype_FK");

                entity.HasOne(d => d.DepartureLocation)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.DepartureLocationId)
                    .HasConstraintName("shipment_Location_FK");
            });

            modelBuilder.Entity<ShipmentItem>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("ShipmentItem_PK")
                    .IsClustered(false);

                entity.ToTable("ShipmentItem");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.ItemDescription).HasColumnName("itemDescription");

                entity.Property(e => e.ItemNature).HasColumnName("itemNature");

                entity.Property(e => e.ItemWeight)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("itemWeight");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.ShipmentItems)
                    .HasForeignKey(d => d.ShipmentId)
                    .HasConstraintName("ShipmentItem_shipment_FK");
            });

            modelBuilder.Entity<ShipmentItemCategory>(entity =>
            {
                entity.ToTable("ShipmentItemCategory");

                entity.Property(e => e.MaxValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaxWeight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinWeight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PriceValue).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ShipmentPackaging>(entity =>
            {
                entity.ToTable("ShipmentPackaging");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Createdby).HasMaxLength(255);

                entity.Property(e => e.CreationTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PackagingFee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ShipmentId).HasMaxLength(255);
            });

            modelBuilder.Entity<ShipmentParcel>(entity =>
            {
                entity.ToTable("ShipmentParcel");

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.AssignedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DispatchRiderCommission).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsDroppedOffDate).HasColumnType("datetime");

                entity.Property(e => e.IsPickedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ShipmentRequest>(entity =>
            {
                entity.ToTable("ShipmentRequest");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DropOffState).HasMaxLength(50);

                entity.Property(e => e.PackagePiece).HasMaxLength(50);

                entity.Property(e => e.PackageWeight).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.PickupDate).HasColumnType("datetime");

                entity.Property(e => e.PickupState).HasMaxLength(50);

                entity.Property(e => e.PickupTime).HasMaxLength(50);

                entity.Property(e => e.TruckNumber).HasMaxLength(50);

                entity.Property(e => e.TruckSize).HasMaxLength(50);
            });

            modelBuilder.Entity<ShipmentTracking>(entity =>
            {
                entity.HasKey(e => e.TrackingId);

                entity.ToTable("ShipmentTracking");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Waybill).HasMaxLength(100);
            });

            modelBuilder.Entity<Smsprofile>(entity =>
            {
                entity.ToTable("SMSProfile");

                entity.Property(e => e.AppName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(36);

                entity.Property(e => e.Port).HasMaxLength(36);

                entity.Property(e => e.Profile).HasMaxLength(36);

                entity.Property(e => e.SmsminQty).HasColumnName("SMSMinQty");

                entity.Property(e => e.Smspassword)
                    .HasMaxLength(36)
                    .HasColumnName("SMSPassword");

                entity.Property(e => e.SmssubUserName)
                    .HasMaxLength(36)
                    .HasColumnName("SMSsubUserName");

                entity.Property(e => e.SmsuserName)
                    .HasMaxLength(36)
                    .HasColumnName("SMSUserName");

                entity.Property(e => e.SmtpAddress).HasMaxLength(36);

                entity.Property(e => e.Username).HasMaxLength(36);
            });

            modelBuilder.Entity<SpecialPackage>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("SpeciaPackage_PK")
                    .IsClustered(false);

                entity.ToTable("SpecialPackage");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ItemWeight)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("itemWeight");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<SpecialPackagepricing>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("SpecialPackagepricing_PK")
                    .IsClustered(false);

                entity.ToTable("SpecialPackagepricing");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Desription).HasColumnName("desription");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.SpecialPackageId).HasColumnName("specialPackageId");

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("weight");

                entity.Property(e => e.ZoneId).HasColumnName("zoneId");

                entity.HasOne(d => d.SpecialPackage)
                    .WithMany(p => p.SpecialPackagepricings)
                    .HasForeignKey(d => d.SpecialPackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SpecialPackagepricing_SpeciaPackage_FK");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.SpecialPackagepricings)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SpecialPackagepricing_Zone_FK");
            });

            modelBuilder.Entity<SpecialShipmentPrice>(entity =>
            {
                entity.ToTable("SpecialShipmentPrice");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PricePerKg)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PricePerKG");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("State_Region_FK");
            });

            //modelBuilder.Entity<State1>(entity =>
            //{
            //    entity.HasKey(e => new { e.JobId, e.Id })
            //        .HasName("PK_HangFire_State");

            //    entity.ToTable("State", "HangFire");

            //    entity.Property(e => e.Id).ValueGeneratedOnAdd();

            //    entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            //    entity.Property(e => e.Name).HasMaxLength(20);

            //    entity.Property(e => e.Reason).HasMaxLength(100);
            //});

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");
            });

            modelBuilder.Entity<SubRoute>(entity =>
            {
                entity.ToTable("SubRoute");
            });

            modelBuilder.Entity<Terminal>(entity =>
            {
                entity.ToTable("Terminal");

                entity.Property(e => e.OnlineDiscount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("Trip");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TripAvailability>(entity =>
            {
                entity.ToTable("TripAvailability");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TripSetting>(entity =>
            {
                entity.ToTable("TripSetting");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.Property(e => e.Imeinumber).HasColumnName("IMEINumber");
            });

            modelBuilder.Entity<VehicleAllocationDetail>(entity =>
            {
                entity.ToTable("VehicleAllocationDetail");
            });

            modelBuilder.Entity<VehicleMake>(entity =>
            {
                entity.ToTable("VehicleMake");
            });

            modelBuilder.Entity<VehicleMileage>(entity =>
            {
                entity.HasKey(e => new { e.VehicleRegistrationNumber, e.ServiceLevel });

                entity.ToTable("VehicleMileage");
            });

            modelBuilder.Entity<VehicleModel>(entity =>
            {
                entity.ToTable("VehicleModel");
            });

            modelBuilder.Entity<VehiclePart>(entity =>
            {
                entity.ToTable("VehiclePart");
            });

            modelBuilder.Entity<VehiclePartPosition>(entity =>
            {
                entity.ToTable("VehiclePartPosition");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<VehiclePartRegistration>(entity =>
            {
                entity.ToTable("VehiclePartRegistration");
            });

            modelBuilder.Entity<VehicleTripRegistration>(entity =>
            {
                entity.ToTable("VehicleTripRegistration");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Vehiclemodel1>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Vehiclemodels_PK")
                    .IsClustered(false);

                entity.ToTable("Vehiclemodels");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MakeId).HasColumnName("makeId");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.Make)
                    .WithMany(p => p.Vehiclemodel1s)
                    .HasForeignKey(d => d.MakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Vehiclemodels_VehicleMake_FK");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("Vendor");
            });

            modelBuilder.Entity<VendorInformation>(entity =>
            {
                entity.ToTable("VendorInformation");

                entity.Property(e => e.AccountBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.GlpurchaseAccount).HasColumnName("GLPurchaseAccount");

                entity.Property(e => e.ReferalUrl).HasColumnName("ReferalURL");

                entity.Property(e => e.TaxGroupId).HasColumnName("TaxGroupID");

                entity.Property(e => e.TaxIdno).HasColumnName("TaxIDNo");

                entity.Property(e => e.TermsId).HasColumnName("TermsID");

                entity.Property(e => e.VattaxIdnumber).HasColumnName("VATTaxIDNumber");

                entity.Property(e => e.VendorIndustryId).HasColumnName("VendorIndustryID");

                entity.Property(e => e.VendorRegionId).HasColumnName("VendorRegionID");

                entity.Property(e => e.VendorSourceId).HasColumnName("VendorSourceID");

                entity.Property(e => e.VendorTypeId).HasColumnName("VendorTypeID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");
            });

            modelBuilder.Entity<VolumeRange>(entity =>
            {
                entity.ToTable("VolumeRange");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.FixedPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PricePerKm).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WeightPriceHighest).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WeightPriceLevel1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WeightPriceLevel2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WeightPriceLevel3).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OldBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WalletNumber).HasMaxLength(450);
            });

            modelBuilder.Entity<WalletNumber>(entity =>
            {
                entity.ToTable("WalletNumber");

                entity.Property(e => e.WalletPan).HasMaxLength(450);
            });

            modelBuilder.Entity<WalletTransaction>(entity =>
            {
                entity.ToTable("WalletTransaction");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreditAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DebitAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LineBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TransactionAmount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.Property(e => e.CoscontrolAccount).HasColumnName("COSControlAccount");
            });

            modelBuilder.Entity<WarehouseBin>(entity =>
            {
                entity.Property(e => e.WarehouseBinIdcode).HasColumnName("WarehouseBinIDCode");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");
            });

            modelBuilder.Entity<WayBill>(entity =>
            {
                entity.HasKey(e => e.Int)
                    .HasName("wayBill_PK")
                    .IsClustered(false);

                entity.ToTable("wayBill");

                entity.Property(e => e.Int)
                    .ValueGeneratedNever()
                    .HasColumnName("int");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.LocationId).HasColumnName("locationId");

                entity.Property(e => e.WayBillCode)
                    .HasMaxLength(100)
                    .HasColumnName("wayBillCode");
            });

            modelBuilder.Entity<WeightRange>(entity =>
            {
                entity.ToTable("WeightRange");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.LowerRange).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpperRange).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Workshop>(entity =>
            {
                entity.ToTable("Workshop");
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Zone_PK")
                    .IsClustered(false);

                entity.ToTable("Zone");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.ZonePrice).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ZoneMapping>(entity =>
            {
                entity.ToTable("ZoneMapping");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DepartuteId).HasColumnName("departuteId");

                entity.Property(e => e.DestinationId).HasColumnName("destinationId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ZoneId).HasColumnName("zoneId");

                entity.HasOne(d => d.Departute)
                    .WithMany(p => p.ZoneMappingDepartutes)
                    .HasForeignKey(d => d.DepartuteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZoneMapping_Location");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.ZoneMappingDestinations)
                    .HasForeignKey(d => d.DestinationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZoneMapping_Location1");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.ZoneMappings)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZoneMapping_Zone");
            });

            modelBuilder.Entity<ZonePrice>(entity =>
            {
                entity.ToTable("ZonePrice");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("weight");

                entity.Property(e => e.ZoneId).HasColumnName("zoneId");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.ZonePrices)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZonePrice_Zone");
            });

            modelBuilder.Entity<ZonePricePerKg>(entity =>
            {
                entity.ToTable("ZonePricePerKg");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("dateCreated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasColumnName("dateModified");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.PricePerKg)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("pricePerKg");

                entity.Property(e => e.ZoneId).HasColumnName("zoneId");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.ZonePricePerKgs)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZonePricePerKg_ZonePricePerKg");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
