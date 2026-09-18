# RentCar – Araç Kiralama API'si

Clean Architecture prensiplerine göre katmanlara ayrılmış bir araç kiralama (Rent-A-Car) projesi. Backend ASP.NET Core Web API, arayüz için React + TypeScript projesi başlatılmıştır.

## Özellikler

- Versiyonlu API (`/api/v1/...`)
- Araç listeleme, detay, ekleme ve silme
- Araç kiralama ve teslim alma (müsaitlik kontrolü ile)
- DTO kullanımı (`RentCar.Shared`)
- Uygulama açılışında veritabanının otomatik oluşturulması (`EnsureCreated`) ve örnek araç verisi (BMW X5, Mercedes E300, Audi A6)
- React uygulaması (`http://localhost:3000`) için CORS politikası
- Swagger dokümantasyonu

## Teknolojiler

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core (SQLite)
- MediatR, FluentValidation (Application katmanında referanslı)
- Swagger (Swashbuckle)
- React 19 + TypeScript (Create React App)

## Mimari

```
RentCar/
├── RentCar.Domain/          # Entity'ler: Car, Customer, Rental, Payment, BaseEntity
├── RentCar.Application/     # Uygulama katmanı (MediatR, FluentValidation)
├── RentCar.Infrastructure/  # AppDbContext (EF Core)
├── RentCar.Shared/          # DTO'lar (CarDto, CreateCarDto)
├── RentCar.API/             # Controllers/v1/CarsController, Program.cs
└── rentcar-ui/              # React + TypeScript arayüz (geliştirme aşamasında)
```

İlişkiler: `Car 1─N Rental`, `Customer 1─N Rental`, `Rental 1─1 Payment`

## API Uç Noktaları

| Metot | Adres | Açıklama |
|-------|-------|----------|
| GET | `/api/v1/Cars` | Tüm araçları listeler |
| GET | `/api/v1/Cars/{id}` | Araç detayı |
| POST | `/api/v1/Cars` | Yeni araç ekler |
| POST | `/api/v1/Cars/{id}/rent` | Aracı kiralar (müsait değilse `400`) |
| PUT | `/api/v1/Cars/{id}/return` | Aracı teslim alır |
| DELETE | `/api/v1/Cars/{id}` | Aracı siler |

## Kurulum ve Çalıştırma

### Backend

```bash
git clone https://github.com/smlmrt/rent-car-app.git
cd rent-car-app
dotnet restore
cd RentCar.API && dotnet run
```

- API: `http://localhost:5120`
- Swagger: `http://localhost:5120/swagger`

Veritabanı (`RentCar.db`) ilk çalıştırmada otomatik oluşturulur; migration çalıştırmaya gerek yoktur.

### Frontend

```bash
cd rentcar-ui
npm install
npm start
```

Arayüz `http://localhost:3000` adresinde açılır.

> Not: `rentcar-ui` klasörü bu depoda ayrı bir git deposu (submodule bağlantısı) olarak yer aldığı için içeriği GitHub'da görüntülenmez. Arayüz şu an Create React App başlangıç şablonu aşamasındadır.
