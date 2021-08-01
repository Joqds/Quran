import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';
import 'package:joqds_quran/ui/widgets/persian_text_input_formatter.dart';
import 'package:persian_number_utility/persian_number_utility.dart';

class QuranPageScreen extends StatefulWidget {
  const QuranPageScreen({Key? key}) : super(key: key);

  @override
  State<QuranPageScreen> createState() => _QuranPageScreenState();
}

class _QuranPageScreenState extends State<QuranPageScreen> {
  final TextEditingController textEditingController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        SizedBox(
          width: 150,
          child: TextField(
            // de: InputDecoration(contentPadding: const EdgeInsets.symmetric(vertical: 40.0)),
            // scrollPadding: ,
            controller: textEditingController,
            keyboardType: const TextInputType.numberWithOptions(
                decimal: false, signed: false),
            textAlign: TextAlign.center,
            textInputAction: TextInputAction.done,
            onSubmitted: (value) {
              goToRead(context, int.tryParse(value.toEnglishDigit()));
            },
            inputFormatters: [
              PersianDigitOnlyTextInputFormatterWithRange(min: 0, max: 604)
            ],
            style: Theme.of(context).textTheme.headline3,
            decoration: InputDecoration(
              labelText: "شماره صفحه",
              labelStyle: Theme.of(context).textTheme.headline5,
              helperText: "بین 1 تا 604".toPersianDigit(),
              helperStyle: Theme.of(context).textTheme.headline5,
              alignLabelWithHint: true,
            ),
          ),
        ),
        Padding(
          padding: const EdgeInsets.only(top: 10.0),
          child: ElevatedButton(
              style: ButtonStyle(
                  fixedSize:
                      MaterialStateProperty.all(const Size.fromWidth(150)),
                  textStyle:
                      MaterialStateProperty.all(const TextStyle(fontSize: 20))),
              onPressed: () => goToRead(context,
                  int.parse(textEditingController.text.toEnglishDigit())),
              child: const Text("برو به صفحه")),
        )
      ],
    );
  }

  goToRead(BuildContext context, int? pageId) {
    if (pageId != null) {
      BlocProvider.of<NavBloc>(context)
          .add(GoRead(type: ReadViewType.page, id: pageId, context: context));
    }
  }
}
