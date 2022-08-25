# ChatService

Server'ı ayağa kaldırdıktan sonra bir ya da birden fazla client projesi instance alarak test edebilirsiniz.
Client projesi ayağa kaldırıldığında sunucuya bağlanır, sunucu ekranında istemcinin bağlandığına dair mesaj görebilirsiniz.
Client sunucuya bağlandıktan sonra sizden mesajınızı girmenizi bekler. Mesaj girildiğinde girilen mesaja sunucuya iletilir.
Sürekli bağlanan client'i dinlemekte olan sunucu mesajınızı okur sunucu ekranına basar ve varsa gönderici client dışındaki tüm client'lara mesajı iletir.
Client sunucu bağlandıktan sonra dinleme moduna geçer ve sunucudan gelen mesajları ekrana, mesajın gönderilme tarihi ile birlikte basar.