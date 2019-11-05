<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="html"/>
  <xsl:template match="/SITMailRelease">
    <html>
      <head>
        <title></title>
        <style type="text/css">
          body{
          font-family:Arial, Helvetica, sans-serif,"宋体";
          background:#e8e8e8;
          font-size:14px;
          }


          td{
          font-size:14px;
          padding:5px;

          font-family:Arial, Helvetica, sans-serif,"宋体";
          }

          .MailItemKey
          {
          font-size:12px;
          font-weight:bold;
          }
        </style>
      </head>
      <body>
       <table id="table1" width="750" cellspacing="0" cellpadding="0" >
          <tr>
            <td colspan="2">
              Dear <xsl:value-of select="Name_Eng"/>,
            </td>
          </tr>
          <tr>
            <td colspan="2" height="20">

            </td>
          </tr>
          <tr>
            <td height="24" width="30"></td>
            <td>
              您已經被<xsl:value-of select="Duty"/>設置為代理人，時間為<xsl:value-of select="BeginDate"/>---<xsl:value-of select="EndDate"/>

            </td>
          </tr>
         
          
          <tr>
            <td height="44" colspan="2"> Best Regards!</td>
          </tr>

          <tr height="25">
            <td colspan="2">
              <xsl:value-of select="DateNow" />
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
