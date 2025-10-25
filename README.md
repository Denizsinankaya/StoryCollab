# StoryCollab - Ortak Hikaye Yazma Platformu

**StoryCollab** kullanıcıların hikayeler oluşturabileceği, bölümler ekleyebileceği ve hikayelere katkıda bulunabileceği bir platformdur. Bu proje, **Admin MVC Paneli** ve **Web API**'den oluşmaktadır.

## **Özellikler**

- **Kullanıcı Kimlik Doğrulama**: JWT tabanlı kimlik doğrulama.
- **Hikaye Yönetimi**: Hikayeler oluşturma ve yönetme.
- **Bölüm Yönetimi**: Hikayelere bölüm ekleyip, bölümleri yayınlama.
- **Katkıcı Yönetimi**: Hikayelere katkıcı ekleyebilme.
- **Yetkilendirme**: Sadece sahipler ve katkıcılar hikayeleri ve bölümleri değiştirebilir.
- **Swagger UI**: API uç noktalarını kolayca test etme.

## **Kullanılan Teknolojiler**

- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core (EF Core)
- SQL Server
- Swagger API Dokümantasyonu
- JWT Kimlik Doğrulama

## **Proje Kurulumu**

### **Gereksinimler**

- **.NET 6.0** (veya daha yüksek bir sürüm)
- **SQL Server** (veritabanı için)
- **Visual Studio** veya uyumlu herhangi bir IDE
- **Swagger** (API testleri için)


Veritabanı Kurulumu

SQL Server'ın kurulu ve çalışır durumda olduğundan emin olun.

appsettings.json dosyasını açın ve connection string kısmını aşağıdaki gibi yapılandırın:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=StoryCollabDb;User Id=your_username;Password=your_password;"
}


Veritabanını oluşturmak için migration işlemini çalıştırın:

dotnet ef database update

Uygulama Çalıştırma

Uygulamayı derleyip çalıştırın:

dotnet run


Web API https://localhost:7112/api/ adresinde ve MVC Admin Panel https://localhost:7154/ adresinde çalışacaktır.

Swagger UI

API dokümantasyonu ve test için Swagger UI'yi kullanabilirsiniz:

https://localhost:7112/swagger


Swagger UI üzerinden API uç noktalarını test edebilirsiniz.

API Uç Noktaları
Kimlik Doğrulama

POST /api/auth/login: Giriş yapın ve JWT token'ı alın.

Hikaye Uç Noktaları

GET /api/stories: Tüm hikayeleri listeleyin (giriş yapmış kullanıcılar için erişilebilir).

POST /api/stories: Yeni bir hikaye oluşturun (JWT token gereklidir).

GET /api/stories/{id}: Belirli bir hikayenin detaylarını alın.

POST /api/stories/{id}/contributors: Bir hikayeye katkıcı ekleyin.

Bölüm Uç Noktaları

POST /api/chapters: Bir hikayeye yeni bir bölüm ekleyin.

PUT /api/chapters/{id}/publish: Bir bölümü yayınlayın.

Admin MVC Panel Kullanımı

Giriş Yapın: İlk olarak Giriş sayfasını kullanarak giriş yapın. Başarılı bir girişten sonra JWT token'ı session içinde saklanır.

Yeni Hikaye Oluşturun: "Yeni Hikaye" butonuna tıklayarak yeni bir hikaye oluşturabilirsiniz.

Katkıcı Ekleyin: Hikayelere katkıcı eklemek için "Katkıcı Ekle" butonunu kullanın.

Bölüm Ekleyin: "Bölüm Ekle" sayfasına giderek bir hikayeye bölüm ekleyebilirsiniz.

Bölümü Yayınlayın: Bir taslak bölüm oluşturduktan sonra, "Yayınla" butonuna tıklayarak bölümü yayınlayabilirsiniz.

Notlar

JWT Token: Giriş yaptıktan sonra token'ı session veya authorization header içinde saklayın ve API isteklerinde kullanın.

Yetkilendirme: Sistem rol tabanlı yetkilendirme kullanmaktadır. Yalnızca hikaye sahipleri ve katkıcılar hikayeleri veya bölümleri değiştirebilir.

### **Repository'yi Klonlama**

Öncelikle, repository'yi bilgisayarınıza klonlayın:

```bash
git clone https://github.com/Denizsinankaya/storycollab.git


