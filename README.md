# SimpleObs
SimpleObs reposu .Net Core ile hazırlanmış API' lara istek yapan Asp.Net Core arayüzlerden oluşan basit bir öğrenci-not senaryosunun demosudur.

![Build Status](http://img.shields.io/travis/badges/badgerbadgerbadger.svg?style=flat-square)

# Kullanılan Teknolojiler

- .Net Core 3.1
- Identity
- EntityFrameworkCore
- SQLite
- Automapper
- Swagger
---

## Örnek

![Öğrenci Sınavlar](https://github.com/codebycinar/SimpleObs/blob/master/img/Notes1.PNG)

![Öğrenci Ders Sonuç](https://github.com/codebycinar/SimpleObs/blob/master/img/Notes2.PNG)

![Admin Tüm Öğrenciler](https://github.com/codebycinar/SimpleObs/blob/master/img/Notes3.PNG)
---

## Yükleme

- Repository'deki tüm proje `code` ları çalıştırılmalıdır.

### Clone

- Bilgisayarınıza `https://github.com/codebycinar/SimpleObs.git` adresinden clone alabilirsiniz.

### Kurulum

- Proje codefirst yaklaşımı ile hazırlanmıştır. Bu nedenle Application altındaki Data klasöründe SchoolDb.db veritabanı dosyası bulunmalıdır. Eğer yoksa Migration ve Database Update işlemleri yapılmalıdır. Uygulama çalıştırıldığında, Database seed olacaktır.

---

## Kullanım
- Database seed işlemi sonrasında aşağıdaki datalar oluşur
```
16 sınıf (1'den 4'e - A,B,C,D şubeleri), 
3 Ders (Türkçe, Matematik, Fen Bilimleri),
12 Sınav (Yazılı ve Sözlü sınavlar),
180 Öğrenci
```

- Her öğrenci için abc ile başlayan kullanıcı adı oluşur, hepsinin şifresi 123456 'dır.
```
Örnek :
Username : abc1
Password : 123456

Username : abc2
Password : 123456
```
- Admin kullanıcısı tüm öğrencilere ait dataları görüntüleyebilmektedir.
```
Username : admin
Password : 123456
```
---

## Contributing

> To get started...

### Step 1

- **Option 1**
    - 🍴 Fork this repo!

- **Option 2**
    - 👯 Clone this repo to your local machine using `https://github.com/codebycinar/SimpleObs.git`

### Step 2

- **HACK AWAY!** 🔨🔨🔨

### Step 3

- 🔃 Create a new pull request using <a href="https://github.com/codebycinar/SimpleObs/compare/" target="_blank">`https://github.com/codebycinar/SimpleObs/compare/`</a>.

---

## FAQ

- **How do I do *specifically* so and so?**
    - No problem! Just do this.

---

## License

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

- **[MIT license](http://opensource.org/licenses/mit-license.php)**
- Copyright 2020 © <a href="https://github.com/codebycinar/" target="_blank">Hüseyin Çınar</a>.
