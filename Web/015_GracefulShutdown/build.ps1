$image = 'graceful-shutdown'
$timestamp = GET-DATE -Format "yyyyMMdd-HHmm"
$tag = "$($timestamp)"

$fullImage = "$($image):$($tag)"

docker build --no-cache -t $fullImage .\WebApplication1\