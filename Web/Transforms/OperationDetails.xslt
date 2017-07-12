<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/"
                xmlns:soap="http://schemas.xmlsoap.org/wsdl/soap/" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <xsl:param name="operationName" />
  <xsl:param name="productName" />
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
            <xsl:value-of select="$operationName"/>
          </h2>
          <h3>Description</h3>
          <p class="intro">
            <xsl:value-of select="wsdl:portType/wsdl:operation[@name=$operationName]/wsdl:documentation/comments/summary"/>
          </p>
          <p class="intro">
            <xsl:value-of select="wsdl:portType/wsdl:operation[@name=$operationName]/wsdl:documentation/comments/remarks"/>
          </p>
          <xsl:if test="wsdl:portType/wsdl:operation[@name=$operationName]/wsdl:documentation/comments/example">
            <h3>Example(s)</h3>
            <pre>
              <xsl:value-of select="wsdl:portType/wsdl:operation[@name=$operationName]/wsdl:documentation/comments/example"/>
            </pre>
          </xsl:if>
        </span>
        <span>
          <h3>Parameters</h3>
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
            <xsl:for-each select="/wsdl:definitions/wsdl:types//xsd:element[@name=$operationName]//xsd:element">
              <tr>
                <xsl:variable name="requestType" select="@type" />
                <xsl:variable name="paramName" select="@name" />
                <xsl:variable name="requestTypeDisplay" select="substring-after($requestType, ':')" />
                <td>
                  <xsl:if test="substring-before($requestType, ':') = 'xsd'">
                    <xsl:if test="@nillable">
                      <xsl:value-of select="$requestTypeDisplay" />?
                    </xsl:if>
                    <xsl:if test="not(@nillable)">
                      <xsl:value-of select="$requestTypeDisplay" />
                    </xsl:if>
                  </xsl:if>
                  <xsl:if test="substring-before($requestType, ':') != 'xsd'">
                    <a>
                      <xsl:attribute name="href">
                        ?datacontract=<xsl:value-of select="$requestType"/>
                      </xsl:attribute>
                      <xsl:if test="@nillable">
                        <xsl:value-of select="$requestTypeDisplay" />?
                      </xsl:if>
                      <xsl:if test="not(@nillable)">
                        <xsl:value-of select="$requestTypeDisplay" />
                      </xsl:if>
                    </a>
                  </xsl:if>
                </td>
                <td>
                  <xsl:value-of select="$paramName" />
                  -
                  <i>
                    <xsl:value-of select="/wsdl:definitions/wsdl:portType/wsdl:operation[@name=$operationName]/wsdl:documentation/comments/param[@name=$paramName]"/>
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
          
          <h3>Return Value</h3>
          <p>
          <xsl:variable name="operationResponse" select="concat($operationName,'Response')" />
          <xsl:variable name="responseType" select="/wsdl:definitions/wsdl:types//xsd:element[@name=$operationResponse]//xsd:element/@type" />
          <xsl:variable name="responseTypeDisplay" select="substring-after($responseType, ':')" />
          <xsl:if test="substring-before($responseType, ':') = 'xsd'">
            <xsl:value-of select="$responseTypeDisplay" />
          </xsl:if>
          <xsl:if test="substring-before($responseType, ':') != 'xsd'">
            <a>
              <xsl:attribute name="href">
                ?datacontract=<xsl:value-of select="$responseType"/>
              </xsl:attribute>
              <xsl:value-of select="$responseTypeDisplay" />
            </a>
          </xsl:if>
            -
            <i>
              <xsl:value-of select="wsdl:portType/wsdl:operation[@name=$operationName]/wsdl:documentation/comments/returns"/>
            </i>
          </p>
        </span>
      </div>
    </body>
  </html>
  </xsl:template>

</xsl:stylesheet>
