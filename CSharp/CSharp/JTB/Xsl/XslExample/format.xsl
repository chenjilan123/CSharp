<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:output method="html"/>
<xsl:template match="/SITMailRelease">
  <html>
    <head>
      <title></title>
      <style type="text/css">
        body
        {
        font-family: Arial, Helvetica, sans-serif, "宋体";
        font-size: 14px;
        }

        td
        {
        font-size: 14px;
        padding: 5px;
        font-family: Arial, Helvetica, sans-serif, "宋体";
        }

        .MailItemKey
        {
        font-size: 14px;
        font-weight: bold;
        font-family: Arial, Helvetica, sans-serif, "宋体";
        }
      </style>
    </head>
    <body>
      <table align="center">
        <tr>
          <td>

            <table d="table1"  cellspacing="0" cellpadding="0" width="1000px" style="border: solid 1px #BACF84">
              <tr>
                <td width="50"> </td>
                <td align="right" style="font-family: Arial, Helvetica, sans-serif; font-size: x-large; font-weight: normal; color: #336600; font-style: italic;">
                  SW SIT Mail Release System
                </td>
                <td width="50">
                </td>
              </tr>
              <tr>
                <td>
                </td>
                <td>Dear All,</td>
                <td></td>
              </tr>
              <tr>
                <td colspan="3" height="15">
                </td>
              </tr>
              <tr>
                <td ></td>
                <td>
                  The following images are released.
                </td>
                <td></td>
              </tr>
              <tr>
                <td height="15px"></td>
                <td></td>
                <td></td>
              </tr>
              <tr>
                <td></td>
                <td>

                  <table width="100%" cellspacing="0" cellpadding="0" >
                    <tr bgcolor="#F0F7E1">
                      <td width="250"  class="MailItemKey">
                        Compal Project Code :

                      </td>

                      <td width="700" >
                        <xsl:value-of select="ProjectCode"/>

                      </td>
                    </tr>

                    <tr bgcolor="#FFFFFF">
                      <td  text-align="center"  class="MailItemKey">
                        Compal Project Name :

                      </td>

                      <td  text-align="center">

                        <xsl:value-of select="ProjectName"/>
                      </td>
                    </tr>
                    <tr bgcolor="#F0F7E1">

                      <td  text-align="center"  class="MailItemKey">
                        Customer Code :

                      </td>

                      <td  text-align="center">
                        <xsl:value-of select="CustomerCode"/>

                      </td>
                    </tr>
                    <tr bgcolor="#FFFFFF">
                      <td  text-align="center"  class="MailItemKey">
                        Image Owner :

                      </td>

                      <td  text-align="center">

                        <xsl:value-of select="ImageOwner"/>
                      </td>
                    </tr>

                    <tr  bgcolor="#F0F7E1">

                      <td  text-align="center"  width="250"  class="MailItemKey">
                        Image Release Date :
                      </td>

                      <td  test-align="center" width="700">
                        <xsl:value-of select="ReleaseDate"/>
                      </td>
                    </tr>

                    <tr bgcolor="#FFFFFF">

                      <td  text-align="center"  class="MailItemKey">
                        Files Server Saved Path :
                      </td>

                      <td  text-align="center">
                        <xsl:value-of select="FTPPath"/>
                      </td>
                    </tr>
                    <tr bgcolor="#F0F7E1">

                      <td  text-align="center"  class="MailItemKey">
                        Files Server UserName :
                      </td>

                      <td  text-align="center">
                        <xsl:value-of select="FTPUser"/>

                      </td>
                    </tr>
                    <tr  bgcolor="#FFFFFF">

                      <td  text-align="center"  class="MailItemKey">
                        Files Server UserPwd :

                      </td>

                      <td  text-align="center">

                        <xsl:value-of select="FTPPassword"/>
                      </td>
                    </tr>

                    <tr bgcolor="#F0F7E1">

                      <td  text-align="center"  class="MailItemKey">
                        <font color="red">
                          Release Note :
                        </font>
                      </td>

                      <td  text-align="center" wrap="true">


                        <font color="red">
                          <xsl:value-of select="Remark" disable-output-escaping="yes"></xsl:value-of>
                        </font>

                      </td>
                    </tr>
                  </table>


                </td>
                <td></td>
              </tr>
              <tr>
                <td height="20"></td>
                <td></td>
                <td></td>
              </tr>
              
              <tr>
                <td></td>
                <td>

                  Image information :
                </td>
                <td></td>
              </tr>
              
              <tr>
                <td>

                </td>
                <td>
                  <table id="table10" cellpadding="0" cellspacing="0"  style="border: solid 1px #F0F7E1" >

                    <tr >

                      <td  class="MailItemKey" bgcolor="#F0F7E1" >
                        Platform
                      </td>
                      <td class="MailItemKey" bgcolor="#F0F7E1">
                        Language
                      </td>
                      <td class="MailItemKey" bgcolor="#F0F7E1">
                        Image ID
                      </td>
                      <td  class="MailItemKey" bgcolor="#F0F7E1">
                        OS
                      </td>
                      <td class="MailItemKey" bgcolor="#F0F7E1">
                        Block
                      </td>
                      <td class="MailItemKey" bgcolor="#F0F7E1">
                        BIOS Version
                      </td>

                    </tr>
                    <xsl:for-each select="MailContent">
                      <tr>
                        <td style="border: solid 1px #F0F7E1">
                          <xsl:value-of select="Platform"/>
                        </td>
                        <td style="border: solid 1px #F0F7E1">
                          <xsl:value-of select="Language"/>
                        </td>
                        <td style="border: solid 1px #F0F7E1">
                          <xsl:value-of select="ImageID"/> 
                        </td>
                        <td style="border: solid 1px #F0F7E1">
                          <xsl:value-of select="OS"/>
                        </td>
                        <td style="border: solid 1px #F0F7E1">
                          <xsl:value-of select="Block"/>
                        </td>
                        <td style="border: solid 1px #F0F7E1">
                          <xsl:value-of select="BIOSVersion"/>
                        </td>

                      </tr>
                    </xsl:for-each>
                  </table>
                </td>
                <td></td>
              </tr>
              <tr>
                <td height="30"></td>
                <td></td>
                <td></td>
              </tr>
              <tr>
                <td>

                </td>
                <td style="font-style:italic">
                  Best Regards!
                </td>
                <td></td>
              </tr>
              <tr>
                <td>

                </td>
                <td style="font-style:italic">
                  <xsl:value-of select="DateNow" />
                </td>
                <td></td>
              </tr>
              <tr>
                <td height="30"></td>
                <td></td>
                <td></td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </body>
  </html>
  

</xsl:template>
</xsl:stylesheet>