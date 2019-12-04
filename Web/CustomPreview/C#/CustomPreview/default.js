
function bodyResize() {
    resizeBan();
}
function resizeBan() {


    if (msieversion() > 4) {
        try {


            if (document.body.clientWidth == 0) return;


            var oBanner = document.all.item("pagetop");

            var oText = document.all.item("pagebody");
            if (oText == null) return;
            var oBannerrow1 = document.all.item("projectnamebanner");
            var oTitleRow = document.all.item("pagetitlebanner");
            if (oBannerrow1 != null) {
                var iScrollWidth = dxBody.scrollWidth;
                oBannerrow1.style.marginRight = 0 - iScrollWidth;
            }
            if (oTitleRow != null) {
                oTitleRow.style.padding = "0px 10px 0px 22px; ";
            }
            if (oBanner != null) {
                document.body.scroll = "no"
                oText.style.overflow = "auto";
                oBanner.style.width = document.body.offsetWidth - 2;
                oText.style.paddingRight = "20px"; // Width issue code
                oText.style.width = document.body.offsetWidth - 4;
                oText.style.top = 0;
                if (document.body.offsetHeight > oBanner.offsetHeight)
                    oText.style.height = document.body.offsetHeight - (oBanner.offsetHeight + 4)
                else oText.style.height = 0
            }
            try { nstext.setActive(); } //allows scrolling from keyboard as soon as page is loaded. Only works in IE 5.5 and above.
            catch (e) { }

        }
        catch (e) { }
    }
}

function msieversion()
// Return Microsoft Internet Explorer (major) version number, or 0 for others.
// This function works by finding the "MSIE " string and extracting the version number
// following the space, up to the decimal point for the minor version, which is ignored.
{
    var ua = window.navigator.userAgent
    var msie = ua.indexOf("MSIE ")

    if (msie > 0)        // is Microsoft Internet Explorer; return version number
        return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)))
    else
        return 0    // is other browser
}


function bodyLoad() {

    resizeBan();
    document.body.onresize = bodyResize;
}
