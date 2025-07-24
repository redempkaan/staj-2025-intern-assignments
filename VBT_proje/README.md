# Staj Backend Projesi

Bu proje, temel kullanıcı yönetimi işlevlerine sahip (Register, Login, Logout) basit bir backend uygulamasıdır. Staj süresince backend geliştirme becerilerini geliştirmek ve temel servislerin nasıl çalıştığını öğrenmek amacıyla hazırlanmıştır.

## İşlevler

- Kullanıcı kaydı (Register)
- Kullanıcı girişi (Login)
- Kullanıcı çıkışı (Logout)

## Kullanılan Teknolojiler

- C#
- .NET
- JWT tabanlı kimlik doğrulama
- MS SQL

## API Endpointleri

| Yöntem | Yol         | Açıklama               |
|--------|-------------|------------------------|
| POST   | /register   | Kullanıcı kaydı        |
| POST   | /login      | Giriş işlemi           |
| POST   | /logout     | Çıkış işlemi           |

## Senaryo

- Register işlevini kullanarak veri tabanına yeni bir kullanıcı girdisi oluşturun.
- Oluşturduğunuz girdiyle login olun.
- Login ile birlikte gelen JWT tokenini girin ve logout ile çıkış yapın.

## Notlar

Kendi çalıştırma ortamınızda denemeden önce settings dosyasındaki bağlantı stringine kendi veri tabanınızın konfigürasyonuna uygun bir string yazmayı ve migrate etmeyi unutmayın.


Katkı yapmak isterseniz lütfen bir fork oluşturun ve pull request açın.

Bu proje MIT lisansı ile lisanslanmıştır.
