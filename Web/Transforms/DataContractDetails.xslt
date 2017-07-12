<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/"
                xmlns:soap="http://schemas.xmlsoap.org/wsdl/soap/" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <xsl:param name="datacontractName" />
  <xsl:param name="productName" />
  <xsl:variable name="operationName" select="'test'" />
  <xsl:variable name="datacontractDisplayName" select="substring-after($datacontractName, ':')" />
  <xsl:template match="/wsdl:definitions">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
      <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
      <link rel="stylesheet" type="text/css" href="Documentation.css" />
      <title>
        <xsl:value-of select="$productName"/>:
        <xsl:value-of select="wsdl:service/@name"/> SOAP Web Service
      </title>
    </head>
    <body>
      <div id="content">
        <p class="heading1">
          <xsl:value-of select="$productName"/>:
          <xsl:value-of select="wsdl:service/@name"/> SOAP Web Service
        </p>
        <br/>
        <span>
          <p class="intro">
            Click <a href="?">here</a> for a complete list of operations.
          </p>
          <h2>
            <xsl:value-of select="$datacontractDisplayName"/>
          </h2>
          <p class="intro">
            <xsl:value-of select="wsdl:types//xsd:complexType[@name=$datacontractDisplayName]/xsd:annotation/xsd:documentation/comments/summary"/>
          </p>
          <p class="intro">
            <xsl:value-of select="wsdl:types//xsd:complexType[@name=$datacontractDisplayName]/xsd:annotation/xsd:documentation/comments/remarks"/>
          </p>
        </span>
        <span>
          <h3>Base Classes</h3>
          <xsl:if test="not(wsdl:types//xsd:complexType[@name=$datacontractDisplayName]/xsd:complexContent/xsd:extension[@base])">
            <p>
              None
            </p>
          </xsl:if>
          <xsl:if test="wsdl:types//xsd:complexType[@name=$datacontractDisplayName]/xsd:complexContent/xsd:extension[@base]">
            <p>
              This class extends the following base classes:
            </p>
            <ul>
              <xsl:for-each select="wsdl:types//xsd:complexType[@name=$datacontractDisplayName]/xsd:complexContent/xsd:extension[@base]">
                <li>
                  <xsl:variable name="baseClass" select="@base" />
                  <xsl:variable name="baseClassDisplay" select="substring-after($baseClass, ':')" />
                  <a>
                    <xsl:attribute name="href">
                      ?datacontract=<xsl:value-of select="$baseClass"/>
                    </xsl:attribute>
                    <xsl:value-of select="$baseClassDisplay" />
                  </a>
                </li>
              </xsl:for-each>
            </ul>
          </xsl:if>
          <h3>Properties</h3>
          <p>
            The following properties are provided directly by this class:
          </p>
          <table>
            <tr>              
              <th>
                Type
              </th>
              <th>
                Name                
              </th>
              <th>
                Required
              </th>
            </tr>
            <xsl:for-each select="wsdl:types//xsd:complexType[@name=$datacontractDisplayName]//xsd:element">
              <tr>
                <xsl:variable name="propertyType" select="@type" />
                <xsl:variable name="paramName" select="@name" />
                <xsl:variable name="propertyTypeDisplay" select="substring-after($propertyType, ':')" />
                <td>
                  <xsl:if test="substring-before($propertyType, ':') = 'xsd'">
                    <xsl:if test="@nillable">
                      <xsl:value-of select="$propertyTypeDisplay" />?
                    </xsl:if>
                    <xsl:if test="not(@nillable)">
                      <xsl:value-of select="$propertyTypeDisplay" />
                    </xsl:if>
                  </xsl:if>
                  <xsl:if test="substring-before($propertyType, ':') != 'xsd'">
                    <a>
                      <xsl:attribute name="href">
                        ?datacontract=<xsl:value-of select="$propertyType"/>
                      </xsl:attribute>
                      <xsl:if test="@nillable">
                        <xsl:value-of select="$propertyTypeDisplay" />?
                      </xsl:if>
                      <xsl:if test="not(@nillable)">
                        <xsl:value-of select="$propertyTypeDisplay" />
                      </xsl:if>
                    </a>
                  </xsl:if>
                  <xsl:if test="@maxOccurs = 'unbounded'">
                    []
                  </xsl:if>
                </td>
                <td>
                  <xsl:value-of select="$paramName" />
                  -
                  <i>
                    <xsl:value-of select="xsd:annotation/xsd:documentation/comments/summary"/>
                    <xsl:if test="xsd:annotation/xsd:documentation/comments/remarks">
                      (
                    </xsl:if>
                    <xsl:value-of select="xsd:annotation/xsd:documentation/comments/remarks"/>
                    <xsl:if test="xsd:annotation/xsd:documentation/comments/remarks">
                      )
                    </xsl:if>
                  </i>
                </td>
                <td>
                  <xsl:if test="not(@nillable)">
                    Yes
                  </xsl:if>
                </td>
              </tr>
            </xsl:for-each>
          </table>
        </span>
      </div>
    </body>
  </html>
  </xsl:template>

</xsl:stylesheet>
