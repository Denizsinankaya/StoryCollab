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

### **Repository'yi Klonlama**

Öncelikle, repository'yi bilgisayarınıza klonlayın:

```bash
git clone https://github.com/Denizsinankaya/storycollab.git
