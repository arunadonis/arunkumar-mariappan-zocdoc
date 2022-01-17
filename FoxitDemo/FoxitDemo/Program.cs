using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using foxit;
using foxit.common;
using foxit.common.fxcrt;
using foxit.pdf;
using foxit.pdf.interform;
using foxit.pdf.annots;
using foxit.pdf.actions;

namespace FoxitDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string sn = "pYpqrgJrB6LC2Z68PeFwLtCYCLxHxcf+k11jhpaG3EuLMxmfuzr8dQ==";
            string key = "8f0YFUGMvRkN+ddwhrBxubTv1X8GzmILMEDAsAA2gnuto3FG9BCVyYfdywgxSAlo0ZqMg8ysK14l4c3FL6BsyZH1nsZGHTmYPwTThEG6DUeelRyBi+EM+YZCnKCIDlhUi9USxVaSLrKUK00+QwHUKi18pfpYswG0/YtkZM+uSf9ML8rintL8IYuNvydGovn9j1J2cUby2KikZxeQ5KCuuAyJLJSF2qG/cjSZPKyHNSetI6dIZ5/92919k2R0xKsMEWKlJOjLx+o8mSuO4CTP5RTUfXlGdRaD/0jfE4kUIZpMYC0cvLfpBBSeuvWFdFNyWus1rfW9JnHw0jiLxCC01UYfn+JNXldYCQ3V/LAPyvjxwWM0aF9usnMSVuH8FPQWYo+HrZC9e9mDPd14ta9eN8Hb0b72Rz+Efz++krReyh8LAttHdTDZPpydgF9poTsOBYvXg9KDs0R+ksyyDHNtwoI8eagAz3pYperNMW4k1qZFUvwyxut0hyix56KghWow+5PfcdN4+ujzoi/aO8nl1+ghiiQ2U1Vl94ZhK3KrGyBEnDlp8yfpf66wzdh8yPXo/QSRttWhadiDgoz7b1YB4k6efjL2c+iaYCjUknauauYXoBZ9vGgW8RMx6htPSJZl2xXM7ccoha9nHsH0KbK1wgn7D/yHfChibO2LALVrBvVMTmy/jO6gzw5ROZwXTIcIV7dpqhAvkEkWxvlko3sgF2mSAQahscNyfI5XE3xMJS9DjSIRLy+aW+f8jsg6pTKlarreXe0PMmWq13qDo2nj3wbIgOjNoWvokYVtsu7684pRFIQUR2FOb3HchPOi5gCWo15JwckB57DaRy2IXrunKHLTi87MESB8UZBZkDtFI5I0ySfCvWE45VHpSDuZXNMRD/GjJGUw7qh6QzzMcftOO/+ACAhtdAg9AKG6cweeEKyItc06c4YSIcgC9Bsk+gEwvUXvuClJS8VaCtk0SyViByXb21smG2BlRP0n6yWGRIvcLBSyd0XcC6jL2vw3NSyruKY+b9pFVk1u7Z8e2gBgqxG5xQg1PU2diuZVlgzI+uwxbz2HnNvPsfJTrmwI9B8HTy7Z+WP4Nr2L3+CzICifC3/04GK0d+fMaMhJu3Fr6B97ImU9KOlWF6N4KqOZb74PWrn+cArAZAf495PwIMYq0hegEViI2EQyBkS2X4vwMB+PKAchATtICmX0L12XNZ75SPIISKK6nN94EvDl2z7a1CYq01+nA09Adwo2aRvY0JjJDZwrzAGijXDelMya6Ry/OFC9awNS9lRDh+yGiZrWFOXJWBB4U47WTWMsXnAMlSWVd4qJnrSDBIY=";
            // Initialize library
            ErrorCode error_code = Library.Initialize(sn, key);
            if (error_code != ErrorCode.e_ErrSuccess)
            {
                Console.WriteLine("Library Initialize Error: {0}\n", error_code);
                Library.Release();
                return;
            }

            try
            {
                string filename = "DemographicForm.pdf";
                using (PDFDoc doc = new PDFDoc(filename))
                {
                    error_code = doc.Load(null);
                    if (error_code != ErrorCode.e_ErrSuccess) return;
                    // Get the first page of the document.
                    PDFPage page = doc.GetPage(0);
                    // Parse page.
                    Progressive prog = page.StartParse((int)foxit.pdf.PDFPage.ParseFlags.e_ParsePageNormal, null, false);
                    int rate = prog.GetRateOfProgress();
                    while (rate < 100) {
                        System.Threading.Thread.Sleep(10);
                    }
                    Form form = new Form(doc);
                    // Add text field
                    Control control = form.AddControl(page, "patientname", Field.Type.e_TypeTextField, new RectF(70f, 700f, 350f, 720f));
                    using (Field field = control.GetField())
                    {
                        field.SetValue("John Doe");
                        // Update text field's appearance.
                        using (Widget widget = control.GetWidget())
                            widget.ResetAppearanceStream();
                    }
                    using (Control rb1 = form.AddControl(page, "minor", Field.Type.e_TypeCheckBox, new RectF(142f, 619f, 152f, 630f)))
                    using (Control rb2 = form.AddControl(page, "single", Field.Type.e_TypeCheckBox, new RectF(214f, 619f, 224f, 630f)))
                    using (Control rb3 = form.AddControl(page, "married", Field.Type.e_TypeCheckBox, new RectF(286f, 619f, 296f, 630f)))
                    using (Control rb4 = form.AddControl(page, "divorced", Field.Type.e_TypeCheckBox, new RectF(358f, 619f, 368f, 630f)))
                    using (Control rb5 = form.AddControl(page, "widowed", Field.Type.e_TypeCheckBox, new RectF(430f, 619f, 440f, 630f)))
                    using (Control rb6 = form.AddControl(page, "separated", Field.Type.e_TypeCheckBox, new RectF(502f, 619f, 512f, 630f)))
                    {
                        rb3.SetChecked(true);
                        // Update radio button's appearance.
                        using (Widget widget = rb1.GetWidget())
                            widget.ResetAppearanceStream();

                        // Update radio button's appearance.
                        using (Widget widget = rb2.GetWidget())
                            widget.ResetAppearanceStream();

                        // Update radio button's appearance.
                        using (Widget widget = rb3.GetWidget())
                            widget.ResetAppearanceStream();

                        // Update radio button's appearance.
                        using (Widget widget = rb4.GetWidget())
                            widget.ResetAppearanceStream();

                        // Update radio button's appearance.
                        using (Widget widget = rb5.GetWidget())
                            widget.ResetAppearanceStream();

                        // Update radio button's appearance.
                        using (Widget widget = rb6.GetWidget())
                            widget.ResetAppearanceStream();
                    }

                    page.Flatten(true, (int)PDFPage.FlattenOptions.e_FlattenAll);
                    doc.SaveAs("TestForm.pdf", (int)PDFDoc.SaveFlags.e_SaveFlagNoOriginal);
                }
            }
            catch (foxit.PDFException e)
            {
                Console.WriteLine(e.Message);
            }
            Library.Release();
        }
    }
}
