# Genetik Algoritma ile Matyas Fonksiyonu Hesaplama



#### Genetik-Algoritma Arayüz
![Arayüz](/gui.png)



### Nasıl çalışır?

Kendi bildiğim kadarıyla oluşturmuş olduğum kod ve çalışma prebsibi bu şekildedir hatalı veya eksiklik olabilir arkadaşlar kullanacaklar mutlaka kendi bilgileri ile mukayese etsinler.

Buradaki mantık üretilen popülasyon gen sayısı içerisinden search domaini sınırları içindeki sayılar arasında rastgele gen üretiyoruz bu matyas için -10 ve 10 aralığında olmakta. Elitizim ile belirtilen miktarda en iyi hedef fonksiyonumuzun optimum sonucuna yaklaştıran genleri saklamak. Burada kullanlan formül matyas fonksiyonu. En iyi sonuç f(0,0)= 0 aralığında veriyor yani gen değerlerimiz bu formüle göre 0 a yaklaştıkça hedef olan sonuca yaklaşmış oluyoruz ve yüksek bir skor elde etmiş oluyoruz. Bu işlem en iyi skoru veren genleri saklayarak iterasyon kadar tekrar etmekte ve genlerin arasından en yüksek skor veren geni yani 0 a en yakın olan gen olmuş oluyor bu. Bunu da yakınsama grafiği üzerinde göstermek. 
 
Extra olarak bunu System.Drawing içindeki çizim araçlarııyla şablon bir görsel üzerinde elde edilen genlerin koordinatlarıyla çizilmesini sağlayarak gerçekten istenen şekilde çalışıp çalışmadığını görebilir ve düzeltmelerinizi ona göre tekrardan değiştirebilirsiniz. Bunun için arkadaki şablonun formülünüzün üstten görünümü hangisi ise onunla değiştirmeniz gerekmektedir.


#### Matyas Function
![Matyas Function](https://upload.wikimedia.org/wikipedia/commons/thumb/6/63/Matyas_function.pdf/page1-1200px-Matyas_function.pdf.jpg)
![Matyas Function](/matyasFunction.png)


## Uygulanması

Verilen parametrelere göre yakınsama grafiği çizdirilir ve popülasyon tabloya render edilerek görüntülenmesi sağlanır.

#### Formul
```
f(X)=0.26*((x1*x1)+(x2*x2))−0.48*x1*x2
```

#### Search Domain
```
−10≤xi≤10 , i=1,2
fmin(X∗)=0
x∗i=0
```
