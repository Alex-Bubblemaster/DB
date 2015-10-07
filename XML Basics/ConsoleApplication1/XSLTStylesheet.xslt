<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      <body>
        <h1>Telerik Students</h1>
        <table border="1">
          <xsl:for-each select="students/student">
            <tr>
              <td>
                <xsl:value-of select="name"/>
              </td>
              <td>
                <xsl:value-of select="sex"/>
              </td>
              <td>
                <xsl:value-of select="dob"/>
              </td>
              <td>
                <xsl:value-of select="phone"/>
              </td>
              <td>
                <xsl:value-of select="email"/>
              </td>
            </tr>
            <tr>
              <td>
                <xsl:value-of select="course"/>
              </td>
              <td>
                <xsl:value-of select="specialty"/>
              </td>
            <td>
                <xsl:value-of select="faculty-number"/>
              </td>
            </tr>
            <tr>
              <td align="center">
                <ul>
                <xsl:for-each select="students/student">
                  <li><xsl:value-of select ="name"/></li>
                  <li><xsl:value-of select="tutor"/></li>
                  <li><xsl:value-of select="score"/></li>
                </xsl:for-each>
              </ul>
              </td>       
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
