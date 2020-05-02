#$openSSLArgs = "req -x509 -days 365 -newkey rsa:2048 -keyout web-key.pem -out web-cert.pem"
#Start-Process openssl $openSSLArgs


$openSSLArgs = "pkcs12 -export -in web-cert.pem -inkey web-key.pem -out web-certificate.pfx"
Start-Process openssl $openSSLArgs