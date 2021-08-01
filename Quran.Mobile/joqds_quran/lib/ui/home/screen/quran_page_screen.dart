import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';
import 'package:persian_number_utility/persian_number_utility.dart';

class QuranPageScreen extends StatefulWidget {
  const QuranPageScreen({Key? key}) : super(key: key);

  @override
  State<QuranPageScreen> createState() => _QuranPageScreenState();
}

class _QuranPageScreenState extends State<QuranPageScreen> {
  final TextEditingController textEditingController =
      TextEditingController(text: "1");

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(20.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.center,
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          TextField(
            controller: textEditingController,
            keyboardType: TextInputType.number,
            textAlign: TextAlign.center,
            textInputAction: TextInputAction.go,
            inputFormatters: [FilteringTextInputFormatter.digitsOnly],
            decoration: const InputDecoration(labelText: "شماره صفحه"),
          ),
          Padding(
            padding: const EdgeInsets.only(top: 10.0),
            child: ElevatedButton(
                onPressed: () =>
                    goToRead(context, int.parse(textEditingController.text)),
                child: const Text("برو به صفحه")),
          )
        ],
      ),
    );
  }

  goToRead(BuildContext context, int rubId) {
    BlocProvider.of<NavBloc>(context)
        .add(GoRead(type: ReadViewType.rub, id: rubId, context: context));
  }
}
