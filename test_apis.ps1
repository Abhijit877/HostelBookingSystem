$baseUrl = "https://hostelbookingsystem-production.up.railway.app"

# Generate unique test data
$timestamp = Get-Date -Format "yyyyMMddHHmmss"
$uniqueEmail = "test$timestamp@example.com"
$uniqueName = "Test User $timestamp"

Write-Host "Testing POST API endpoints on Railway.app..." -ForegroundColor Green
Write-Host "Base URL: $baseUrl" -ForegroundColor Yellow
Write-Host "Using unique test data: $uniqueEmail" -ForegroundColor Yellow
Write-Host ""

# First check if database has required data
Write-Host "=== Checking Database Prerequisites ===" -ForegroundColor Magenta

Write-Host "Checking for existing hostels..." -ForegroundColor Cyan
try {
    $response = Invoke-WebRequest -Uri "$baseUrl/api/hostel" -Method GET -TimeoutSec 10
    Write-Host "  Status Code: $($response.StatusCode)" -ForegroundColor Green
    Write-Host "  Response: $($response.Content)" -ForegroundColor White
} catch {
    Write-Host "  Error: $($_.Exception.Message)" -ForegroundColor Red
    if ($_.Exception.Response) {
        Write-Host "  Status Code: $($_.Exception.Response.StatusCode.Value__)" -ForegroundColor Red
    }
}
Write-Host ""

# Test Auth endpoints first
Write-Host "=== Testing Authentication Endpoints ===" -ForegroundColor Magenta

# Register new user
Write-Host "Testing: POST /api/auth/register" -ForegroundColor Cyan
$registerBody = @{ name = $uniqueName; email = $uniqueEmail; password = "password123" } | ConvertTo-Json
try {
    $response = Invoke-WebRequest -Uri "$baseUrl/api/auth/register" -Method POST -Body $registerBody -ContentType "application/json" -TimeoutSec 10
    Write-Host "  Status Code: $($response.StatusCode)" -ForegroundColor Green
    Write-Host "  Response: $($response.Content)" -ForegroundColor White
    $userCreated = $true
} catch {
    Write-Host "  Error: $($_.Exception.Message)" -ForegroundColor Red
    if ($_.Exception.Response) {
        Write-Host "  Status Code: $($_.Exception.Response.StatusCode.Value__)" -ForegroundColor Red
        $errorContent = $_.Exception.Response.GetResponseStream()
        $reader = New-Object System.IO.StreamReader($errorContent)
        $errorResponse = $reader.ReadToEnd()
        Write-Host "  Error Response: $errorResponse" -ForegroundColor Red
    }
    $userCreated = $false
}
Write-Host ""

# Login with created user
if ($userCreated) {
    Write-Host "Testing: POST /api/auth/login" -ForegroundColor Cyan
    $loginBody = @{ email = $uniqueEmail; password = "password123" } | ConvertTo-Json
    try {
        $response = Invoke-WebRequest -Uri "$baseUrl/api/auth/login" -Method POST -Body $loginBody -ContentType "application/json" -TimeoutSec 10
        Write-Host "  Status Code: $($response.StatusCode)" -ForegroundColor Green
        Write-Host "  Response: $($response.Content)" -ForegroundColor White
    } catch {
        Write-Host "  Error: $($_.Exception.Message)" -ForegroundColor Red
        if ($_.Exception.Response) {
            Write-Host "  Status Code: $($_.Exception.Response.StatusCode.Value__)" -ForegroundColor Red
            $errorContent = $_.Exception.Response.GetResponseStream()
            $reader = New-Object System.IO.StreamReader($errorContent)
            $errorResponse = $reader.ReadToEnd()
            Write-Host "  Error Response: $errorResponse" -ForegroundColor Red
        }
    }
    Write-Host ""
}

# Test other POST endpoints
Write-Host "=== Testing Other POST Endpoints ===" -ForegroundColor Magenta

# Create Room
Write-Host "Testing: POST /api/room" -ForegroundColor Cyan
$roomBody = @{ hostelId = 1; roomNumber = "101"; type = 0; price = 50.00; isAvailable = $true; description = "Test room" } | ConvertTo-Json
try {
    $response = Invoke-WebRequest -Uri "$baseUrl/api/room" -Method POST -Body $roomBody -ContentType "application/json" -TimeoutSec 10
    Write-Host "  Status Code: $($response.StatusCode)" -ForegroundColor Green
    Write-Host "  Response: $($response.Content)" -ForegroundColor White
    $roomCreated = $true
    $roomData = $response.Content | ConvertFrom-Json
    $roomId = $roomData.id
} catch {
    Write-Host "  Error: $($_.Exception.Message)" -ForegroundColor Red
    if ($_.Exception.Response) {
        Write-Host "  Status Code: $($_.Exception.Response.StatusCode.Value__)" -ForegroundColor Red
        $errorContent = $_.Exception.Response.GetResponseStream()
        $reader = New-Object System.IO.StreamReader($errorContent)
        $errorResponse = $reader.ReadToEnd()
        Write-Host "  Error Response: $errorResponse" -ForegroundColor Red
    }
    $roomCreated = $false
}
Write-Host ""

# Create Booking (if room was created)
if ($roomCreated) {
    Write-Host "Testing: POST /api/booking" -ForegroundColor Cyan
    $bookingBody = @{ userId = 1; roomId = $roomId; checkInDate = "2024-12-15T10:00:00Z"; checkOutDate = "2024-12-16T10:00:00Z"; totalAmount = 50.00; status = 0 } | ConvertTo-Json
    try {
        $response = Invoke-WebRequest -Uri "$baseUrl/api/booking" -Method POST -Body $bookingBody -ContentType "application/json" -TimeoutSec 10
        Write-Host "  Status Code: $($response.StatusCode)" -ForegroundColor Green
        Write-Host "  Response: $($response.Content)" -ForegroundColor White
        $bookingCreated = $true
        $bookingData = $response.Content | ConvertFrom-Json
        $bookingId = $bookingData.id
    } catch {
        Write-Host "  Error: $($_.Exception.Message)" -ForegroundColor Red
        if ($_.Exception.Response) {
            Write-Host "  Status Code: $($_.Exception.Response.StatusCode.Value__)" -ForegroundColor Red
            $errorContent = $_.Exception.Response.GetResponseStream()
            $reader = New-Object System.IO.StreamReader($errorContent)
            $errorResponse = $reader.ReadToEnd()
            Write-Host "  Error Response: $errorResponse" -ForegroundColor Red
        }
        $bookingCreated = $false
    }
    Write-Host ""
}

# Create Payment (if booking was created)
if ($bookingCreated) {
    Write-Host "Testing: POST /api/payment" -ForegroundColor Cyan
    $paymentBody = @{ bookingId = $bookingId; amount = 50.00; paymentMethod = "Credit Card"; status = 0; transactionId = "TXN$timestamp" } | ConvertTo-Json
    try {
        $response = Invoke-WebRequest -Uri "$baseUrl/api/payment" -Method POST -Body $paymentBody -ContentType "application/json" -TimeoutSec 10
        Write-Host "  Status Code: $($response.StatusCode)" -ForegroundColor Green
        Write-Host "  Response: $($response.Content)" -ForegroundColor White
        $paymentCreated = $true
        $paymentData = $response.Content | ConvertFrom-Json
        $paymentId = $paymentData.id
    } catch {
        Write-Host "  Error: $($_.Exception.Message)" -ForegroundColor Red
        if ($_.Exception.Response) {
            Write-Host "  Status Code: $($_.Exception.Response.StatusCode.Value__)" -ForegroundColor Red
            $errorContent = $_.Exception.Response.GetResponseStream()
            $reader = New-Object System.IO.StreamReader($errorContent)
            $errorResponse = $reader.ReadToEnd()
            Write-Host "  Error Response: $errorResponse" -ForegroundColor Red
        }
        $paymentCreated = $false
    }
    Write-Host ""
}

# Create Transaction (if payment was created)
if ($paymentCreated) {
    Write-Host "Testing: POST /api/transaction" -ForegroundColor Cyan
    $transactionBody = @{ paymentId = $paymentId; type = 0; amount = 50.00; description = "Room booking payment" } | ConvertTo-Json
    try {
        $response = Invoke-WebRequest -Uri "$baseUrl/api/transaction" -Method POST -Body $transactionBody -ContentType "application/json" -TimeoutSec 10
        Write-Host "  Status Code: $($response.StatusCode)" -ForegroundColor Green
        Write-Host "  Response: $($response.Content)" -ForegroundColor White
    } catch {
        Write-Host "  Error: $($_.Exception.Message)" -ForegroundColor Red
        if ($_.Exception.Response) {
            Write-Host "  Status Code: $($_.Exception.Response.StatusCode.Value__)" -ForegroundColor Red
            $errorContent = $_.Exception.Response.GetResponseStream()
            $reader = New-Object System.IO.StreamReader($errorContent)
            $errorResponse = $reader.ReadToEnd()
            Write-Host "  Error Response: $errorResponse" -ForegroundColor Red
        }
    }
    Write-Host ""
}

# Verify data was saved by checking GET endpoints
Write-Host "=== Verifying Data Persistence (GET Endpoints) ===" -ForegroundColor Magenta

$verifyEndpoints = @("/api/auth/test", "/api/booking", "/api/room", "/api/payment", "/api/transaction")

foreach ($endpoint in $verifyEndpoints) {
    Write-Host "Verifying: GET $endpoint" -ForegroundColor Cyan
    try {
        $response = Invoke-WebRequest -Uri "$baseUrl$endpoint" -Method GET -TimeoutSec 10
        Write-Host "  Status Code: $($response.StatusCode)" -ForegroundColor Green
        Write-Host "  Response: $($response.Content)" -ForegroundColor White
    } catch {
        Write-Host "  Error: $($_.Exception.Message)" -ForegroundColor Red
        if ($_.Exception.Response) {
            Write-Host "  Status Code: $($_.Exception.Response.StatusCode.Value__)" -ForegroundColor Red
        }
    }
    Write-Host ""
}

Write-Host "=== Test Summary ===" -ForegroundColor Magenta
Write-Host "User Registration: $(if ($userCreated) { 'SUCCESS' } else { 'FAILED' })" -ForegroundColor $(if ($userCreated) { 'Green' } else { 'Red' })
Write-Host "User Login: $(if ($userCreated) { 'SUCCESS' } else { 'FAILED' })" -ForegroundColor $(if ($userCreated) { 'Green' } else { 'Red' })
Write-Host "Room Creation: $(if ($roomCreated) { 'SUCCESS' } else { 'FAILED' })" -ForegroundColor $(if ($roomCreated) { 'Green' } else { 'Red' })
Write-Host "Booking Creation: $(if ($bookingCreated) { 'SUCCESS' } else { 'FAILED' })" -ForegroundColor $(if ($bookingCreated) { 'Green' } else { 'Red' })
Write-Host "Payment Creation: $(if ($paymentCreated) { 'SUCCESS' } else { 'FAILED' })" -ForegroundColor $(if ($paymentCreated) { 'Green' } else { 'Red' })
