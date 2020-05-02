
$openSSLArgs = " req -new -x509 -newkey rsa:2048 -keyout webterminal.key -out webterminal.cer -days 365 -subj /CN=webterminal"
Start-Process openssl $openSSLArgs

$openSSLArgs1 = "pkcs12 -export -out certificate.pfx -inkey webterminal.key -in webterminal.cer"
Start-Process openssl $openSSLArgs1