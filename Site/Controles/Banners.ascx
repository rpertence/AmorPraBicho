<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banners.ascx.cs" Inherits="Site.Controles.Banners" %>
<style type="text/css">
    #slidebox {
        position: relative;
        margin-top: 85px;
        margin-left: 5px;
    }

        #slidebox, #slidebox .content {
            width: 165px;
        }

            #slidebox, #slidebox .container, #slidebox .content {
                height: 510px;
            }

    #slidebox {
        overflow: hidden;
    }

        #slidebox .container {
            position: relative;
            left: 0;
        }

        #slidebox .content {
            float: left;
        }

            #slidebox .content div {
                height: 100%;
                font-size: 12pt;
            }

        #slidebox .next, #slidebox .previous {
            position: absolute;
            z-index: 2;
            display: block;
            width: 21px;
            height: 21px;
            margin-top: 240px;
        }

        #slidebox .next {
            right: 0;
            margin-right: 10px;
            background: url(banner/slidebox_next.png) no-repeat left bottom;
        }

            #slidebox .next:hover {
                background: url(banner/slidebox_next_hover.png) no-repeat left bottom;
            }

        #slidebox .previous {
            margin-left: 10px;
            background: url(banner/slidebox_previous.png) no-repeat left bottom;
        }

            #slidebox .previous:hover {
                background: url(banner/slidebox_previous_hover.png) no-repeat left bottom;
            }

        #slidebox .thumbs {
            position: absolute;
            z-index: 2;
            bottom: 10px;
            right: 10px;
        }

            #slidebox .thumbs .thumb {
                display: block;
                margin-left: 5px;
                float: left;
                font-size: 12pt;
                text-decoration: none;
                padding: 2px 4px;
                background: url(banner/slidebox_thumb.png);
                color: #fff;
            }

                #slidebox .thumbs .thumb:hover {
                    background: #fff;
                    color: #000;
                }

        #slidebox .selected_thumb {
            background: #fff;
            color: #000;
            display: block;
            margin-left: 5px;
            float: left;
            font-size: 12pt;
            text-decoration: none;
            padding: 2px 4px;
        }
</style>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"></script>
<script type="text/javascript" src="banner/jquery.easing.1.3.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var autoPlayTime = 3500;
        autoPlayTimer = setInterval(autoPlay, autoPlayTime);
        function autoPlay() {
            Slidebox('next');
        }
        $('#slidebox .next').click(function () {
            Slidebox('next', 'stop');
        });
        $('#slidebox .previous').click(function () {
            Slidebox('previous', 'stop');
        });
        var yPosition = ($('#slidebox').height() - $('#slidebox .next').height()) / 2;
        $('#slidebox .next').css('top', yPosition);
        $('#slidebox .previous').css('top', yPosition);
        $('#slidebox .thumbs a:first-child').removeClass('thumb').addClass('selected_thumb');
        $("#slidebox .content").each(function (i) {
            slideboxTotalContent = i * $('#slidebox').width();
            $('#slidebox .container').css("width", slideboxTotalContent + $('#slidebox').width());
        });
    });

    function Slidebox(slideTo, autoPlay) {
        var animSpeed = 800; //animation speed
        var easeType = 'easeInOutExpo'; //easing type
        var sliderWidth = $('#slidebox').width();
        var leftPosition = $('#slidebox .container').css("left").replace("px", "");
        if (!$("#slidebox .container").is(":animated")) {
            if (slideTo == 'next') { //next
                if (autoPlay == 'stop') {
                    clearInterval(autoPlayTimer);
                }
                if (leftPosition == -slideboxTotalContent) {
                    $('#slidebox .container').animate({ left: 0 }, animSpeed, easeType); //reset
                    $('#slidebox .thumbs a:first-child').removeClass('thumb').addClass('selected_thumb');
                    $('#slidebox .thumbs a:last-child').removeClass('selected_thumb').addClass('thumb');
                } else {
                    $('#slidebox .container').animate({ left: '-=' + sliderWidth }, animSpeed, easeType); //next
                    $('#slidebox .thumbs .selected_thumb').next().removeClass('thumb').addClass('selected_thumb');
                    $('#slidebox .thumbs .selected_thumb').prev().removeClass('selected_thumb').addClass('thumb');
                }
            } else if (slideTo == 'previous') { //previous
                if (autoPlay == 'stop') {
                    clearInterval(autoPlayTimer);
                }
                if (leftPosition == '0') {
                    $('#slidebox .container').animate({ left: '-' + slideboxTotalContent }, animSpeed, easeType); //reset
                    $('#slidebox .thumbs a:last-child').removeClass('thumb').addClass('selected_thumb');
                    $('#slidebox .thumbs a:first-child').removeClass('selected_thumb').addClass('thumb');
                } else {
                    $('#slidebox .container').animate({ left: '+=' + sliderWidth }, animSpeed, easeType); //previous
                    $('#slidebox .thumbs .selected_thumb').prev().removeClass('thumb').addClass('selected_thumb');
                    $('#slidebox .thumbs .selected_thumb').next().removeClass('selected_thumb').addClass('thumb');
                }
            } else {
                var slide2 = (slideTo - 1) * sliderWidth;
                if (leftPosition != -slide2) {
                    clearInterval(autoPlayTimer);
                    $('#slidebox .container').animate({ left: -slide2 }, animSpeed, easeType); //go to number
                    $('#slidebox .thumbs .selected_thumb').removeClass('selected_thumb').addClass('thumb');
                    var selThumb = $('#slidebox .thumbs a').eq((slideTo - 1));
                    selThumb.removeClass('thumb').addClass('selected_thumb');
                }
            }
        }
    }
</script>
<asp:Repeater ID="rptBanner" runat="server" DataSourceID="odsBanner">
    <ItemTemplate>
        <div data-thumb='<%# "App_Themes/ActioAdms/hd/noticias/miniaturas/" + DataBinder.Eval(Container.DataItem, "miniatura") %>' data-src='<%# "App_Themes/ActioAdms/hd/noticias/icones/" + DataBinder.Eval(Container.DataItem, "icone") %>'>
            <div class="camera_caption fadeFromBottom">
                <a href='<%#"NoticiaRCCBH.aspx?id=" + DataBinder.Eval(Container.DataItem, "id") %>'>
                    <%# DataBinder.Eval(Container.DataItem, "titulo")%>' <em><%# DataBinder.Eval(Container.DataItem, "resumo")%>'</em> </a>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<asp:ObjectDataSource ID="odsBanner" runat="server" SelectMethod="NoticiaDestaque" TypeName="Actio.Negocio.Noticias">
    <DeleteParameters>
        <asp:Parameter Name="id" Type="Int32" />
    </DeleteParameters>
</asp:ObjectDataSource>
