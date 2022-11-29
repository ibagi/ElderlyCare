Get-content ..\config\.env.local | foreach {
    $name, $value = $_.split('=')
    Set-Content env:\$name $value
}