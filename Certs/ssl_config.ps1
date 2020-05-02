# choco install OpenSSL.Light -y


if (-not (test-path $profile)){
    New-Item -Path $Profile -Type File -Force
}

'$env:path = "$env:path;C:\Program Files\OpenSSL\bin"' | out-file $profile -Append
'$env:OPENSSL_CONF = "D:\C#_projects\Remote_Vote_System\Certs\openssl.cnf"' | out-file $profile -Append

ise $profile

. $profile

$env:path
$env:OPENSSL_CONF